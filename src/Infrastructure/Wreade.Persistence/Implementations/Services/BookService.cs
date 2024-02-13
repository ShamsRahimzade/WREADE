using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.Utilities.Extensions;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;
using Wreade.Persistence.DAL;

namespace Wreade.Persistence.Implementations.Services
{
	public  class BookService:IBookService
	{
		private readonly AppDbContext _context;
        private readonly IBookRepository _repo;
        private readonly IChapterRepository _chapter;
        private readonly ITagRepository _tag;
        private readonly ICategoryRepository _category;
		private readonly IWebHostEnvironment _env;
		private readonly IHttpContextAccessor _http;
		private readonly IUserService _user;

		public BookService
			(
			AppDbContext context
			,IBookRepository repo
			,IChapterRepository chapter
			,ITagRepository tag
			,ICategoryRepository category
			,IWebHostEnvironment env
			,IHttpContextAccessor http
			,IUserService user
			)
        {
			_context = context;
            _repo = repo;
            _chapter = chapter;
            _tag = tag;
            _category = category;
			_env = env;
			_http = http;
			_user = user;
		}

		public List<Book> GetReadingHistoryForUser(string userName)
		{
			
			var readingHistory = _context.Books.Where(b => b.User.Name == userName).ToList();
			return readingHistory;
		}

		public async Task<BookCreateVM> CreatedAsync(BookCreateVM vm)
		{
			vm.Tags = await _tag.GetAll().ToListAsync();
			vm.Categories = await _category.GetAll().ToListAsync();
			return vm;
		}
		public async Task<PaginationVM<Book>> GetAllAsync(int page=1,int take = 5)
		{
            if (page < 1 || take < 1) throw new Exception("Bad request");
            ICollection<Book> books = await _repo.GetPagination(skip: (page - 1) * take, take: take, orderExpression: x => x.Id, IsDescending: true,includes: new string[] { "BookCategories", "BookCategories.Category","BookTags","BookTags.Tag" }).ToListAsync();
            if (books == null) throw new Exception("Not Found");
            int count = await _repo.GetAll().CountAsync();
            if (count < 0) throw new Exception("Not Found");
            double totalpage = Math.Ceiling((double)count / take);
            PaginationVM<Book> vm = new PaginationVM<Book>
            {
                Items = books,
                CurrentPage = page,
                TotalPage = totalpage
            };
            return vm;
        }
		public async Task<bool> CreateAsync(BookCreateVM vm,ModelStateDictionary ms)
		{
			if (!ms.IsValid) return false;
			if (await _repo.IsExistAsync(l => l.Name == vm.Name))
			{
				ms.AddModelError("Name", "This book is already exist");
				return false;
			}
			AppUser User = await _user.GetUser(_http.HttpContext.User.Identity.Name);
			Book book = new Book
			{
				Name = vm.Name,
				//Chapters = vm.ChapterIds?.Select(id => new Chapter { Id = id }).ToList(),
				CreatedAt = DateTime.UtcNow,
				Description=vm.Description,
				IsCompleted=vm.IsCompleted,
				AppUserId=User.Id,
				CreatedBy= User.UserName,
                BookTags =new List<BookTag>(),
				BookCategories=new List<BookCategory>(),
			};

			if (vm.TagIds is not null)
			{
				foreach (var item in vm.TagIds)
				{
					if (!(await _tag.IsExistAsync(t => t.Id == item)))
					{
						ms.AddModelError(string.Empty, "This tag isn't aviable");
						return false;
					}
					BookTag tag = new BookTag
					{
						TagId = item
					};
					book.BookTags.Add(tag);
				}
			}
			if (vm.CategoryIds is not null)
			{
				foreach (var item in vm.CategoryIds)
				{
					if (!await _category.IsExistAsync(c => c.Id == item))
					{
						ms.AddModelError(string.Empty, "This category isn't aviable");
						return false;
					}
					BookCategory category = new BookCategory
					{
						CategoryId = item
					};
					book.BookCategories.Add(category);
				}
			}
			
			if(vm.CoverPhoto is not null)
			{
				if (!vm.CoverPhoto.CheckType("image/"))
				{
					ms.AddModelError(string.Empty, "wrong type");
					return false;
				}
				if (!vm.CoverPhoto.ValidateSize(7))
				{
					ms.AddModelError(string.Empty, "wrong size");
					return false;
				}
				string file = await vm.CoverPhoto.CreateFileAsync(_env.WebRootPath, "assets", "images");
				book.CoverPhoto = file;
			}
			await _repo.AddAsync(book);
			await _repo.SaveChangeAsync();
			return true;
		}
	}
}

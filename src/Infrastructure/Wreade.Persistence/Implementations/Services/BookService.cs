using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.Utilities.Extensions;
using Wreade.Application.ViewModels;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;
using Wreade.Persistence.DAL;

namespace Wreade.Persistence.Implementations.Services
{
	public  class BookService:IBookService
	{
        private readonly IBookRepository _repo;
        private readonly IChapterRepository _chapter;
        private readonly ITagRepository _tag;
        private readonly ICategoryRepository _category;
		private readonly IWebHostEnvironment _env;
		private readonly IHttpContextAccessor _http;
		private readonly IUserService _user;

		public BookService
			(
			 IBookRepository repo
			,IChapterRepository chapter
			,ITagRepository tag
			,ICategoryRepository category
			,IWebHostEnvironment env
			,IHttpContextAccessor http
			,IUserService user
			)
        {
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
			
			var readingHistory = _repo.GetAllWhere(b => b.User.Name == userName).ToList();
			return readingHistory;
		}
		
		public async Task<List<Book>> GetUserBooksAsync()
		{
			return await _repo.GetOrderBy(b => b.Rating).Take(3).ToListAsync();
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
            ICollection<Book> books = await _repo.GetPagination(skip: (page - 1) * take, take: take,
				orderExpression: x => x.Id, IsDescending: true,
				includes: new string[] { "Chapters", "BookCategories.Category","BookTags","BookTags.Tag", "Libraries" })
				.ToListAsync();
		
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
		public async Task<ICollection<Book>> GetAll(int page = 1, int take = 10)
		{
			return await _repo.GetAll(includes: new string[] { nameof(Book.Chapters)}).ToListAsync();
		}
		public async Task<PaginationVM<Book>> GetBooksCreatedByUserAsync(string userId, int page = 1, int take = 10)
		{


			ICollection<Book> books = await _repo.GetPagination( skip: (page - 1) * take, take: take, orderExpression: c => c.AppUserId == userId, includes:new string[] { "Chapters", "BookCategories.Category", "BookTags", "BookTags.Tag", "Libraries" }).ToListAsync();


			int count = await _repo.GetAll().CountAsync(c => c.AppUserId == userId);
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
			AppUser User = await _user.GetUser(_http.HttpContext.User.Identity.Name, u => u.Followers,
	u => u.Followees,
	u => u.LibraryItems, u => u.Books);
			Book book = new Book
			{
				Name = vm.Name,
				//Chapters = vm.ChapterIds?.Select(id => new Chapter { Id = id }).ToList(),
				CreatedAt = DateTime.UtcNow,
				Description=vm.Description,
				IsCompleted=vm.IsCompleted,
				IsAdult=vm.IsAdult,
				AppUserId =User.Id,
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
		public async Task<bool> DeleteAsync(int id)
		{
			if (id < 1) throw new Exception("id");
			Book exist = await _repo.GetByIdAsync(id);
			if (exist is null) throw new Exception("not found");
			if (exist.Chapters is not null)
			{
				exist.Chapters.RemoveAll(c => c.BookId == exist.Id);
			}
			if (exist.CoverPhoto is not null)
			{
				exist.CoverPhoto.DeleteFile(_env.WebRootPath, "assets", "images");
			}
			_repo.Delete(exist);
			await _repo.SaveChangeAsync();
			return true;
		}
		public async Task<BookUpdateVM> UpdatedAsync(int id, BookUpdateVM vm)
		{
			if (id <= 0) throw new Exception("id");
			Book exist = await _repo.GetByIdAsync(id, includes: new string[] { "BookCategories","BookCategories.Category", "BookTags", "BookTags.Tag", "Libraries" });
			if (exist == null) throw new Exception("not found");
			vm.Image = exist.CoverPhoto;
			vm.Name = exist.Name;
			vm.Description = exist.Description;
			vm.IsCompleted = exist.IsCompleted;
			vm.IsAdult = exist.IsAdult;
			vm.Rating = exist.Rating;
			vm.IsAdult = exist.IsAdult;
			vm.Tags = await _tag.GetAll().ToListAsync();
			vm.TagIds = exist.BookTags.Select(gs => gs.TagId).ToList();
			vm.Categories = await _category.GetAll().ToListAsync();
			vm.TagIds = exist.BookCategories.Select(gs => gs.CategoryId).ToList();
			vm.AppUserId = exist.AppUserId;
			return vm;
		}
		public async Task<bool> UpdateAsync(int id, BookUpdateVM vm, ModelStateDictionary modelstate)
		{
			if (id <= 0) throw new Exception("BadRequest");
			if (!modelstate.IsValid) return false;
			Book book = await _repo.GetByIdAsync(id, includes: new string[] { "BookCategories", "BookCategories.Category", "BookTags", "BookTags.Tag", "Libraries" });
			if (book is null) throw new Exception("not found");
			if (await _repo.IsExistAsync(b => b.Name == vm.Name && b.Id != id ))
			{
				modelstate.AddModelError("Name", "this name is exist");
				return false;
			}

			if (vm.Photo is not null)
			{
				if (!vm.Photo.CheckType("image/"))
				{

					modelstate.AddModelError("Photo", "filetype");
					return false;
				}
				if (!vm.Photo.ValidateSize(7))
				{

					modelstate.AddModelError("Photo", "filesize");
					return false;
				}
				string main = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
				book.CoverPhoto.DeleteFile(_env.WebRootPath, "assets", "images");
				book.CoverPhoto = main;
			}
			if (vm.TagIds != null)
			{
				foreach (var item in vm.TagIds)
				{
					if (!book.BookTags.Any(bt => bt.TagId == item))
					{
						if (!await _tag.IsExistAsync(t => t.Id == item))
						{
							modelstate.AddModelError(String.Empty, "This tag isn't available");
							return false;
						}
						book.BookTags.Add(new BookTag { TagId = item });
					}
				}
				book.BookTags = book.BookTags.Where(bt => vm.TagIds.Any(b => bt.TagId == b)).ToList();
			}
			else
			{
				book.BookTags = new List<BookTag>();
			}
			if (vm.CategoryIds != null)
			{
				foreach (var item in vm.CategoryIds)
				{
					if (!book.BookCategories.Any(bc=> bc.CategoryId == item))
					{
						if (!await _category.IsExistAsync(c => c.Id == item))
						{
							modelstate.AddModelError(String.Empty, "This category isn't available");
							return false;
						}
						book.BookCategories.Add(new BookCategory { CategoryId = item });
					}
				}
				book.BookCategories = book.BookCategories.Where(bc => vm.TagIds.Any(b => bc.CategoryId == b)).ToList();
			}
			else
			{
				book.BookCategories = new List<BookCategory>();
			}
			book.Name = vm.Name;
			book.IsAdult = vm.IsAdult;
			book.IsCompleted = vm.IsCompleted;
			book.Description = vm.Description;
			book.ModifiedAt = DateTime.UtcNow;
			_repo.Update(book);
			await _repo.SaveChangeAsync();
			return true;
		}
		public async Task<BookDetailVM>  DetailAsync(int id,string username)
		{
			if (id <= 0) throw new Exception("Id not found");
			AppUser user = await _user.GetUser(username, u => u.LibraryItems);
			Book book = await _repo.GetByIdAsync(id, includes: new string[] { "BookCategories", "BookCategories.Category", "BookTags", "BookTags.Tag", "Chapters", "Libraries" });
			List<Book> books = await _repo.GetAll( includes: new string[] { "BookCategories", "BookCategories.Category", "BookTags", "BookTags.Tag", "Chapters","Libraries" }).ToListAsync();
			if (book is null) throw new Exception("not found");
			if (user is null) throw new Exception("not found");
			if (books is null) throw new Exception("not found");
			BookDetailVM vm = new BookDetailVM
			{
				user=user,
				Books=books,
				book = book
			};
			return vm;
			
		}
	}
}

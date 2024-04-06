using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.Utilities.Extensions;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
	public class ChapterService : IChapterService
	{
		private readonly IChapterRepository _chaprepo;
		private readonly IWebHostEnvironment _env;
		private readonly IBookRepository _bookrepo;
		private readonly UserManager<AppUser> _user;
		private readonly IHttpContextAccessor _http;
		private readonly ILikeRepository _like;
		private readonly IUserService _service;

		public ChapterService(IChapterRepository chaprepo, IWebHostEnvironment env, IBookRepository bookrepo, UserManager<AppUser> user, IHttpContextAccessor http, ILikeRepository like, IUserService service)
		{
			_chaprepo = chaprepo;
			_env = env;
			_bookrepo = bookrepo;
			_user = user;
			_http = http;
			_like = like;
			_service = service;
		}
		public async Task<CreateChapterVM> CreatedAsync(CreateChapterVM vm)
		{
			//if (id <= 0) throw new Exception("this book is not fount");

			return vm;
		}

		public async Task<bool> CreateAsync(CreateChapterVM vm, ModelStateDictionary ms)
		{
			if (!ms.IsValid) return false;
			if (await _chaprepo.IsExistAsync(c => c.Title == vm.Title))
			{
				ms.AddModelError("Title", "This title is already exist");
				return false;
			}
			string username = "";
			if (_http.HttpContext.User.Identity != null)
			{
				username = _http.HttpContext.User.Identity.Name;
			}
			AppUser User = await _service.GetUser(username, u => u.Followers,
            	u => u.Followees,
            	u => u.LibraryItems, u => u.Books);

			//vm.bookId = id;
			Chapter chapter = new Chapter
			{
				AppUserId = User.Id,
				Title = vm.Title,
				Text = vm.Text,
				CreatedAt = DateTime.UtcNow,
				BookId = vm.bookId

			};
			//chapter= await _chaprepo.GetByExpressionAsync(c=>c.BookId==id);
			if (vm.Image is not null)
			{
				if (!vm.Image.CheckType("image/"))
				{
					ms.AddModelError(string.Empty, "wrong type");
					return false;
				}
				if (!vm.Image.ValidateSize(7))
				{
					ms.AddModelError(string.Empty, "wrong size");
					return false;
				}
				string file = await vm.Image.CreateFileAsync(_env.WebRootPath, "assets", "assets", "img");
				chapter.ChapterImage = file;
			}

			await _chaprepo.AddAsync(chapter);
			await _chaprepo.SaveChangeAsync();
			return true;

		}
		public async Task<ICollection<Chapter>> GetAll(int bookId)
		{
			if (bookId <= 0) throw new Exception("book not found");
			ICollection<Chapter> chaps = await _chaprepo.GetAllWhere(expression: c => c.BookId == bookId, includes: new string[] { nameof(Book) }).ToListAsync();
			return chaps;


		}
		public async Task<PaginationVM<Chapter>> GetAllAsync(int id, int page = 1, int take = 5)
		{
			if (page < 1 || take < 1) throw new Exception("Bad request");
			ICollection<Chapter> chapter = await _chaprepo.GetPagination(
				skip: (page - 1) * take, take: take,
				orderExpression: x => x.Id, IsDescending: true,
				includes: new string[] { nameof(Book) }).ToListAsync();
			ICollection<Book> books = await _bookrepo.GetAllWhere(c => c.Id == id).ToListAsync();
			if (chapter == null) throw new Exception("Not Found");
			int count = await _chaprepo.GetAll().CountAsync();
			if (count < 0) throw new Exception("Not Found");
			double totalpage = Math.Ceiling((double)count / take);
			PaginationVM<Chapter> vm = new PaginationVM<Chapter>
			{
				Items = chapter,
				CurrentPage = page,
				TotalPage = totalpage
			};
			return vm;
		}
		public async Task<PaginationVM<Chapter>> Detail(int id,int bookid, int page = 1, int take = 1)
		{
			if (id <= 0) throw new Exception("Id not found");
			if (page < 1 || take < 1) throw new Exception("Bad request");
			Chapter chapter = await _chaprepo.GetByIdAsync(id, includes: new string[] {   "Likes" });
			ICollection<Chapter> chapters = await _chaprepo.GetPagination(skip: (page - 1) * take, take: take ,expression:c=>c.BookId==bookid, orderExpression: x => x.Id, IsDescending: true, includes: nameof(Book)).ToListAsync();
			if (chapters == null) throw new Exception("Not Found");
			int count = await _chaprepo.GetChapterCountForBookAsync(bookid);
			if (count < 0) throw new Exception("Not Found");
			double totalpage = Math.Ceiling((double)count / take);

			PaginationVM<Chapter> vm = new PaginationVM<Chapter>
			{
				Items=chapters,
				CurrentPage = page,
				TotalPage = totalpage
			};
			return vm;
		}
		public async Task<ChapterDetailVM> Chapter(int id)
		{
			if (id <= 0) throw new Exception("Id not found");
			Chapter chapter = await _chaprepo.GetByIdAsync(id, includes: nameof(Book));
			List<Chapter> chapters = await _chaprepo.GetAll(includes: nameof(Book)).ToListAsync();
			if (chapter is null) throw new Exception("not found");
			if (chapters is null) throw new Exception("not found");
			ChapterDetailVM vm = new ChapterDetailVM
			{
				Chapters = chapters,
				chapter = chapter
			};
			return vm;
		}
		
		public async Task<bool> DeleteAsync(int id)
		{
			if (id < 1) throw new Exception("id");
			Chapter exist = await _chaprepo.GetByIdAsync(id);
			if (exist is null) throw new Exception("not found");

			if (exist.ChapterImage is not null)
			{
				exist.ChapterImage.DeleteFile(_env.WebRootPath, "assets", "assets", "img");
			}
			_chaprepo.Delete(exist);
			await _chaprepo.SaveChangeAsync();
			return true;
		}
		public async Task<UpdateChapterVM> UpdatedAsync(int id, UpdateChapterVM vm)
		{
			if (id <= 0) throw new Exception("id");
			Chapter exist = await _chaprepo.GetByIdAsync(id);
			if (exist == null) throw new Exception("not found");
			vm.Image = exist.ChapterImage;
			vm.Title = exist.Title;
			vm.Text = exist.Text;
			return vm;
		}
		public async Task<bool> UpdateAsync(int id, UpdateChapterVM vm, ModelStateDictionary modelstate)
		{
			if (id <= 0) throw new Exception("BadRequest");
			if (!modelstate.IsValid) return false;
			Chapter chapter = await _chaprepo.GetByIdAsync(id);
			if (chapter is null) throw new Exception("not found");
			if (await _chaprepo.IsExistAsync(c => c.Title == vm.Title && c.Id != id && c.BookId == chapter.BookId))
			{
				modelstate.AddModelError("Title", "this title is exist");
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
				string main = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "assets", "img");
				chapter.ChapterImage.DeleteFile(_env.WebRootPath, "assets", "assets", "img");
				chapter.ChapterImage = main;
			}
			chapter.Title = vm.Title;
			chapter.Text = vm.Text;
			chapter.ModifiedAt = DateTime.UtcNow;
			_chaprepo.Update(chapter);
			await _chaprepo.SaveChangeAsync();
			return true;
		}
		public async Task LikeChapter(int chapid)
		{
			var currentUserId = _http.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _user.Users
				.SingleOrDefaultAsync(u => u.Id == currentUserId);

			var chapter = await _chaprepo.GetByExpressionAsync(c => c.Id == chapid);

			var existingLike = await _like.GetByExpressionAsync(l => l.ChapterId == chapid && l.LikerId == currentUser.Id);

			var like = new Like
			{
				LikerId = currentUser.Id,
				ChapterId = chapid
			};

			await _like.AddAsync(like);

			chapter.LikeCount++;

			_chaprepo.Update(chapter);

			await _chaprepo.SaveChangeAsync();
		}
		public async Task UnlikeChap(int chapid)
		{
			var currentUserId = _http.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
			var currentUser = await _user.Users
				.SingleOrDefaultAsync(u => u.Id == currentUserId);

			var existingLike = await _like.GetByExpressionAsync(l => l.ChapterId == chapid && l.LikerId == currentUser.Id);

			if (existingLike != null)
			{
				_like.Delete(existingLike);

				var chap = await _chaprepo.GetByExpressionAsync(c => c.Id == chapid);
				if (chap != null && chap.LikeCount > 0)
				{
					chap.LikeCount--;
					_chaprepo.Update(chap);
				}

				await _like.SaveChangeAsync();
			}
		}
		public async Task<bool> ViewChapter(int chapterId)
		{
			var chapter = await _chaprepo.GetByIdAsync(chapterId);
			if (chapter == null) throw new Exception();
			

			chapter.ViewCount++; 
			_chaprepo.Update(chapter); 
			await _chaprepo.SaveChangeAsync();
			return true;
		}
		
	}
}

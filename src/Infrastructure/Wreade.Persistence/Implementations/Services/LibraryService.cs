using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;


namespace Wreade.Persistence.Implementations.Services
{
	public class LibraryService : ILibraryService
	{
		private readonly ILibraryRepository _librepo;
		private readonly IBookRepository _bookrepo;
		private readonly IHttpContextAccessor _http;
		private readonly IUserService _user;
		private readonly IBookService _bookser;

		public LibraryService(ILibraryRepository librepo, IBookRepository bookrepo, IHttpContextAccessor http, IUserService user,IBookService bookser)
		{
			_librepo = librepo;
			_bookrepo = bookrepo;
			_http = http;
			_user = user;
			_bookser = bookser;
		}

		public async Task<ICollection<LibraryItemVM>> GetLibraryItem()
		{
			ICollection<LibraryItemVM> libraryItemVM = new List<LibraryItemVM>();
			if (_http.HttpContext.User.Identity.IsAuthenticated)
			{
				AppUser user = await _user.GetUser(_http.HttpContext.User.Identity.Name);
				ICollection<LibraryItem> libraryItems = await _librepo.GetAllWhere(x => x.AppUserId == user.Id, includes: new string[] { nameof(LibraryItem.book) }).ToListAsync();
				libraryItemVM = libraryItems.Select(libraryitem => new LibraryItemVM
				{
					Id = libraryitem.Id,
					AppUserid = libraryitem.AppUserId,
					BookId = libraryitem.BookId,
					book=libraryitem.book
				}).ToList();
			}
			return libraryItemVM;
		}
		public async Task AddBasket(int id,string userId)
		{
			if (id <= 0) throw new Exception("Wrong query");
		
			Book book = await _bookrepo.GetByIdAsync(id,includes:new string[] { "Libraries" });
			if (book == null) throw new Exception("Book not found :(");

			if (!string.IsNullOrEmpty(_http.HttpContext.User.Identity.Name))
			{
				AppUser user = await _user.GetUserById(userId, u => u.Followers,
	u => u.Followees,
	u => u.LibraryItems);
				if (user == null) throw new Exception("User not found:(");

				LibraryItem item = user.LibraryItems.FirstOrDefault(bi => bi.BookId == book.Id);
				if (item == null)
				{
					item = new LibraryItem
					{
						AppUserId = user.Id,
						BookId = book.Id,
						book=book
					};
					user.LibraryItems.Add(item);
				}
				else
				{
					user.LibraryItems.Remove(item);
				}
				
				await _librepo.SaveChangeAsync();
			}
		}

	}
}

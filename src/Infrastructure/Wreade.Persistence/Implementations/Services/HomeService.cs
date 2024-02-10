
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
    internal class HomeService : IHomeService
    {
        private readonly IBookRepository _book;
		private readonly UserManager<AppUser> _userManager;

		public HomeService(IBookRepository book,UserManager<AppUser> userManager)
        {
            _book = book;
			_userManager = userManager;
		}
        public async Task<HomeVM> GetAllAsync()
        {
           List<Book> book=await _book.GetAll().Include(b=>b.User).ToListAsync();
			List<AppUser> users = await _userManager.Users.ToListAsync();
			HomeVM vm = new HomeVM
            {
                Books = book,
                Users=users
            };
             return vm;
           
        }
    }
}

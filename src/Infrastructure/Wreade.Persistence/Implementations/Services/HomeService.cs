
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
      

        public HomeService(IBookRepository book)
        {
            _book = book;
           
        }
        public async Task<HomeVM> GetAllAsync()
        {
           List<Book> book=await _book.GetAll().Include(b=>b.User).Include(b=>b.Images).ToListAsync();
            HomeVM vm = new HomeVM
            {
                Books = book
            };
             return vm;
           
        }
    }
}

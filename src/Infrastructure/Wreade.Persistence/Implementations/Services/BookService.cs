
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.Abstractions.Services;
using Wreade.Domain.Entities;
using Wreade.Persistence.DAL;

namespace Wreade.Persistence.Implementations.Services
{
	public  class BookService:IBookService
	{
		private readonly AppDbContext _context;

		public BookService(AppDbContext context)
        {
			_context = context;
		}

		public List<Book> GetReadingHistoryForUser(string userName)
		{
			
			var readingHistory = _context.Books.Where(b => b.User.Name == userName).ToList();
			return readingHistory;
		}


	}
}

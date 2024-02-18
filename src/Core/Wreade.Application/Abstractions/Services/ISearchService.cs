
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface ISearchService
	{
		Task<List<AppUser>> GetUsers(string username);
		Task<List<Book>> GetBooks(string searchTerm);
		Task<List<Category>> GetCategory(string searchTerm);
		Task<List<Tag>> GetTag(string searchTerm);
	}
}

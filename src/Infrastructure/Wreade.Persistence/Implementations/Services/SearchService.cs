using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
	public class SearchService:ISearchService
	{
		private readonly UserManager<AppUser> _user;
		private readonly IBookRepository _book;
		private readonly ITagRepository _tag;
		private readonly ICategoryRepository _category;

		public SearchService(UserManager<AppUser> user,IBookRepository book,ITagRepository tag,ICategoryRepository category)
        {
			_user = user;
			_book = book;
			_tag = tag;
			_category = category;
		}
		public async Task<List<AppUser>> GetUsers(string searchTerm)
		{

			return await _user.Users.Where(x => x.UserName.ToLower().Contains(searchTerm.ToLower()) || x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Surname.ToLower().Contains(searchTerm.ToLower())).ToListAsync();
		}
		public async Task<List<Book>> GetBooks(string searchTerm)
		{

			return await _book.GetAllWhere(x => x.Name.Contains(searchTerm) ||x.Description.Contains(searchTerm), includes: new string[] { "BookCategories", "BookCategories.Category", "BookTags", "BookTags.Tag", "Chapters" }).ToListAsync();
		}
		public async Task<List<Category>> GetCategory(string searchTerm)
		{
			return await _category.GetAllWhere(x => x.Name.Contains(searchTerm) , includes: new string[] { "BookCategories", "BookCategories.Book" }).ToListAsync();
		}
		public async Task<List<Tag>> GetTag(string searchTerm)
		{
			return await _tag.GetAllWhere(x => x.Name.Contains(searchTerm) , includes: new string[] {  "BookTags", "BookTags.Book"}).ToListAsync();
		}
	}
}

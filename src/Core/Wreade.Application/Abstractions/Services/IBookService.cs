using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wreade.Application.ViewModels;

using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface IBookService
	{
		List<Book> GetReadingHistoryForUser(string userName);
		Task<PaginationVM<Book>> GetAllAsync(int page = 1, int take = 5);
		Task<BookCreateVM> CreatedAsync(BookCreateVM vm);
		Task<bool> CreateAsync(BookCreateVM vm, ModelStateDictionary ms);
		Task<bool> DeleteAsync(int id);

	}
}

﻿using System;
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
		Task<ICollection<Book>> GetAll(int page = 1, int take = 10);

		Task<PaginationVM<Book>> GetAllAsync( int page = 1, int take = 5);
		Task<BookCreateVM> CreatedAsync(BookCreateVM vm);
		Task<bool> CreateAsync(BookCreateVM vm, ModelStateDictionary ms);
		Task<bool> DeleteAsync(int id);
		Task<BookUpdateVM> UpdatedAsync(int id, BookUpdateVM vm);
		Task<bool> UpdateAsync(int id, BookUpdateVM vm, ModelStateDictionary modelstate);
		Task<PaginationVM<Book>> GetBooksCreatedByUserAsync(string userId, int page = 1, int take = 10);
		Task<List<Book>> GetUserBooksAsync();
		Task<PaginationVM<Book>> GetTagId(int id, int page = 1, int take = 10);
		Task<BookDetailVM> DetailAsync(int id, string username);
		Task<PaginationVM<Book>> GetCategoryId(int id, int page = 1, int take = 10);
	}
}

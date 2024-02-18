using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
	public class LibraryService:ILibraryService
	{
		private readonly List<LibraryItemVM> lib = new List<LibraryItemVM>();

		public async Task<List<LibraryItemVM>> GetLibraryAsync(string userId)
		{
			return await Task.FromResult(lib);
		}

		public async Task AddToLibraryAsync(int bookId, string userId)
		{
			lib.Add(new LibraryItemVM { BookId = bookId,  AppUserid= userId });
			await Task.CompletedTask;
		}

		public async Task RemoveFromLibraryAsync(int bookId, string userId)
		{
			var itemToRemove = lib.FirstOrDefault(x => x.BookId == bookId && x.AppUserid == userId);
			if (itemToRemove != null)
			{
				lib.Remove(itemToRemove);
			}
			await Task.CompletedTask;
		}
	}
}

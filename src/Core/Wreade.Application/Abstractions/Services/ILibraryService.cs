using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface ILibraryService
	{
		Task<List<LibraryItemVM>> GetLibraryAsync(string userId);
		Task AddToLibraryAsync(int bookId, string userId);
		Task RemoveFromLibraryAsync(int bookId, string userId);
	}
}

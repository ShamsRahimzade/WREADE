using Wreade.Application.ViewModels;


namespace Wreade.Application.Abstractions.Services
{
	public interface ILibraryService
	{
		Task<ICollection<LibraryItemVM>> GetLibraryItem();
		Task AddBasket(int id, string userId);
	}
}

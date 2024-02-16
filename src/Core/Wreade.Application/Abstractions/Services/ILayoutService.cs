using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface ILayoutService
	{
		Task<Dictionary<string, string>> GetSettingsAsync();
		Task<bool> UpdateSettingAsync(string key, UpdateSettingsVM vm);
		Task<ICollection<Category>> GetCategoriesAsync();

    }
}

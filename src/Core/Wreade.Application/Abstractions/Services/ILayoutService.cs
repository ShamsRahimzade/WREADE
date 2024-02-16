using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wreade.Application.ViewModels;
using Wreade.Application.ViewModels.Setting;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface ILayoutService
	{
		Task<bool> CreateAsync(CreateSettingVM vm, ModelStateDictionary modelstate);

		Task<bool> UpdateAsync(int id, UpdateSettingsVM vm, ModelStateDictionary modelstate);
		Task<UpdateSettingsVM> UpdatedAsync(int id, UpdateSettingsVM vm);
		Task<bool> DeleteAsync(int id);
		Task<PaginationVM<Setting>> GetAllAsync(int page = 1, int take = 10);
		Task<ICollection<Category>> GetCategoriesAsync();
	}
}

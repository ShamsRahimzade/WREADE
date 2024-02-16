using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
	public class LayoutService:ILayoutService
	{
		private readonly ISettingsRepository _settingsRepository;
        private readonly ICategoryService _categoryService;
		private readonly UserManager<AppUser> _useman;

		public LayoutService(ISettingsRepository settingsRepository,ICategoryService categoryService,UserManager<AppUser> useman)
		{
			_settingsRepository = settingsRepository;
            _categoryService = categoryService;
			_useman = useman;
		}

		public async Task<Dictionary<string, string>> GetSettingsAsync()
		{
			var settings = await _settingsRepository.GetAllSettingsAsync();
            return settings.ToDictionary(s => s.Key, s => s.Value);
		}
		public async Task<ICollection<Category>> GetCategoriesAsync()
		{
            return await _categoryService.GetCategoriesAsync();
			
			
        }
	
        public async Task<bool> UpdateSettingAsync(string key,UpdateSettingsVM vm)
		{
			var setting = await _settingsRepository.GetSettingByKeyAsync(key);

			if (setting == null)
			{
				return false; 
			}

			setting.Value = vm.Value; 

			await _settingsRepository.UpdateSettingAsync(setting); 

			return true; 
		}
	}
}

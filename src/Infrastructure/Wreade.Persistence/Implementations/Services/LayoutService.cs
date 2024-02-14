using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;

namespace Wreade.Persistence.Implementations.Services
{
	public class LayoutService:ILayoutService
	{
		private readonly ISettingsRepository _settingsRepository;

		public LayoutService(ISettingsRepository settingsRepository)
		{
			_settingsRepository = settingsRepository;
		}

		public async Task<Dictionary<string, string>> GetSettingsAsync()
		{
			var settings = await _settingsRepository.GetAllSettingsAsync();
			return settings.ToDictionary(s => s.Key, s => s.Value);
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

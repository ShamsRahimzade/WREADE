using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.ViewModels;

namespace Wreade.Application.Abstractions.Services
{
	public interface ILayoutService
	{
		Task<Dictionary<string, string>> GetSettingsAsync();
		Task<bool> UpdateSettingAsync(string key, UpdateSettingsVM vm);
	}
}

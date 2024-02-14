using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Repostories
{
	public interface ISettingsRepository
	{
		Task<IEnumerable<Setting>> GetAllSettingsAsync();
		Task<Setting> GetSettingByKeyAsync(string key);
		Task UpdateSettingAsync(Setting setting);
	}
}

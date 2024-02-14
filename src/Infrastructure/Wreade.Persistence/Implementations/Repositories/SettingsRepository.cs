using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Domain.Entities;
using Wreade.Persistence.DAL;

namespace Wreade.Persistence.Implementations.Repositories
{
	public class SettingsRepository:ISettingsRepository
	{
		private readonly AppDbContext _context;

		public SettingsRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Setting>> GetAllSettingsAsync()
		{
			return await _context.Setting.ToListAsync();
		}
		public async Task<Setting> GetSettingByKeyAsync(string key)
		{
			return await _context.Setting.FirstOrDefaultAsync(s => s.Key == key);
		}
		public async Task UpdateSettingAsync(Setting setting)
		{
			_context.Setting.Update(setting);
			await _context.SaveChangesAsync();
		}
	}
}

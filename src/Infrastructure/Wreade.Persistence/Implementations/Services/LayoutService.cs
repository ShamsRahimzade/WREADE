using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Application.ViewModels.Setting;
using Wreade.Domain.Entities;
using Wreade.Persistence.Implementations.Repositories;

namespace Wreade.Persistence.Implementations.Services
{
	public class LayoutService : ILayoutService
	{
		private readonly ISettingsRepository _repository;
		private readonly IUserService _user;
		private readonly IHttpContextAccessor _accessor;
		private readonly ICategoryService _categoryService;

		public LayoutService(ISettingsRepository repository, IUserService user, IHttpContextAccessor accessor, ICategoryService categoryService)
		{
			_repository = repository;
			_user = user;
			_accessor = accessor;
			_categoryService = categoryService;
		}
		public async Task<bool> CreateAsync(CreateSettingVM vm, ModelStateDictionary modelstate)
		{
			if (!modelstate.IsValid) return false;

			if (await _repository.IsExistAsync(c => c.Key == vm.Key))
			{
				modelstate.AddModelError("Key", "This Setting is already exist");
				return false;
			}
			AppUser User = await _user.GetUser(_accessor.HttpContext.User.Identity.Name, u => u.Followers,
	u => u.Followees,
	u => u.LibraryItems, u => u.Books);
			await _repository.AddAsync(new Setting
			{
				CreatedBy = User.UserName,
				Key = vm.Key,
				Value = vm.Value,

			});
			await _repository.SaveChangeAsync();
			return true;
		}
		public async Task<ICollection<Category>> GetCategoriesAsync()
		{
			return await _categoryService.GetCategoriesAsync();
		}


		public async Task<PaginationVM<Setting>> GetAllAsync(int page = 1, int take = 10)
		{
			ICollection<Setting> categories = await _repository.GetPagination(skip: (page - 1) * take, take: take).ToListAsync();
			int count = await _repository.GetAll().CountAsync();
			double totalpage = Math.Ceiling((double)count / take);
			PaginationVM<Setting> vm = new PaginationVM<Setting>
			{
				Items = categories,
				CurrentPage = page,
				TotalPage = totalpage
			};
			return vm;
		}



		public async Task<bool> UpdateAsync(int id, UpdateSettingsVM vm, ModelStateDictionary modelstate)
		{
			if (id < 1) throw new ArgumentOutOfRangeException("id");
			if (!modelstate.IsValid) return false;

			Setting exist = await _repository.GetByIdAsync(id);
			if (exist == null) throw new Exception("Not found");


			if (await _repository.IsExistAsync(l =>  l.Id != id))
			{
				modelstate.AddModelError("Value", "This Setting is already exist");
				return false;
			}

			exist.Value = vm.Value;

			_repository.Update(exist);
			await _repository.SaveChangeAsync();
			return true;
		}




		public async Task<UpdateSettingsVM> UpdatedAsync(int id, UpdateSettingsVM vm)
		{
			if (id < 1) throw new ArgumentOutOfRangeException("id");
			Setting exist = await _repository.GetByIdAsync(id);
			if (exist == null) throw new Exception("Not found");

			vm.Value = exist.Value;
			return vm;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			if (id < 1) throw new ArgumentOutOfRangeException("id");
			Setting exist = await _repository.GetByIdAsync(id);
			if (exist == null) throw new Exception("Not found");
			_repository.Delete(exist);
			await _repository.SaveChangeAsync();
			return true;
		}
	}
}
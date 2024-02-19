
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.Utilities.Extensions;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryrepo;
		private readonly IWebHostEnvironment _env;

		public CategoryService(ICategoryRepository categoryrepo, IWebHostEnvironment env)
		{
			_categoryrepo = categoryrepo;
			_env = env;
		}

		public async Task<bool> CreateAsync(CategoryCreateVM vm, ModelStateDictionary modelstate)
		{
			if (!modelstate.IsValid) return false;

			if (await _categoryrepo.IsExistAsync(c => c.Name == vm.Name))
			{
				modelstate.AddModelError("Name", "this category is exist");
				return false;
			}
			Category category = new Category
			{
				Name = vm.Name,
				
			};
			if (vm.Photo is not null)
			{

				if (!vm.Photo.CheckType("image/"))
				{

					modelstate.AddModelError("Photo", "filetype");
					return false;
				}
				if (!vm.Photo.ValidateSize(7))
				{

					modelstate.AddModelError("Photo", "filesize");
					return false;
				}
				string main = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");

				category.Image = main;
			}

			await _categoryrepo.AddAsync(category);
			await _categoryrepo.SaveChangeAsync();
			return true;
		}

		public async Task<CategoryCreateVM> CreatedAsync(CategoryCreateVM vm)
		{

			return vm;
		}
		public async Task<ICollection<Category>> GetAll(int page = 1, int take = 10)
		{
			return await _categoryrepo.GetAll(includes:new string[] { "BookCategories", "BookCategories.Book" }).ToListAsync();
		}
		public async Task<PaginationVM<Category>> GetAllAsync(int page = 1, int take = 10)
		{
			ICollection<Category> category = await _categoryrepo.GetPagination(skip: (page - 1) * take, take: take).ToListAsync();
			category = await _categoryrepo.GetOrderBy(c => c.Id).ToListAsync();
			int count = await _categoryrepo.GetAll().Include(c => c.BookCategories).ThenInclude(bc => bc.Book).OrderBy(c => c.Id).CountAsync();
			double totalpage = Math.Ceiling((double)count / take);
			PaginationVM<Category> vm = new PaginationVM<Category>
			{
				Items = category,
				CurrentPage = page,
				TotalPage = totalpage
			};
			return vm;
		}
	

		public async Task<bool> UpdateAsync(int id, CategoryUpdateVM vm, ModelStateDictionary modelstate)
		{
			if (id <= 0) throw new Exception("BadRequest");
			if (!modelstate.IsValid) return false;
			Category category = await _categoryrepo.GetByIdAsync(id);
			if (category is null) throw new Exception("not found");
			if (await _categoryrepo.IsExistAsync(c => c.Name == vm.Name)&&await _categoryrepo.IsExistAsync(c=>c.Id!=id))
			{
				modelstate.AddModelError("Name", "this category is exist");
				return false;
			}

			if (vm.Photo is not null)
			{
				if (!vm.Photo.CheckType("image/"))
				{

					modelstate.AddModelError("Photo", "filetype");
					return false;
				}
				if (!vm.Photo.ValidateSize(7))
				{

					modelstate.AddModelError("Photo", "filesize");
					return false;
				}
				string main = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images");
				category.Image.DeleteFile(_env.WebRootPath, "assets", "images");
				category.Image = main;
			}
			category.Name = vm.Name;
			category.ModifiedAt = DateTime.UtcNow;
			_categoryrepo.Update(category);
			await _categoryrepo.SaveChangeAsync();
			return true;
		}
		public async Task<CategoryDetailVM> DetailAsync(int id)
		{
			if (id <= 0) throw new Exception("Id not found");
			Category category = await _categoryrepo.GetByIdAsync(id, includes: new string[] { "BookCategories", "BookCategories.Book" });
			if (category is null) throw new Exception("not found");
			CategoryDetailVM vm = new CategoryDetailVM
			{
				category = category
			};
			return vm;
		}
		public async Task<CategoryUpdateVM> UpdatedAsync(int id, CategoryUpdateVM vm)
		{
			if (id <=0) throw new Exception("id");
			Category exist = await _categoryrepo.GetByIdAsync(id);
			if (exist == null) throw new Exception("not found");
			vm.Image=exist.Image;
			vm.Name = exist.Name.Trim();

			return vm;
		}
		//public async Task<List<Category>> GetCategory(string searchTerm)
		//{

		//	return await _.Where(x => x.UserName.ToLower().Contains(searchTerm.ToLower()) || x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Surname.ToLower().Contains(searchTerm.ToLower())).ToListAsync();
		//}
		public async Task<bool> DeleteAsync(int id)
		{
			if (id < 1) throw new Exception("id");
			Category exist = await _categoryrepo.GetByIdAsync(id);
			if (exist is null) throw new Exception("not found");
			if (exist.Image is not null)
			{
				exist.Image.DeleteFile(_env.WebRootPath, "assets", "images");
			}
			_categoryrepo.Delete(exist);
			await _categoryrepo.SaveChangeAsync();
			return true;
		}
		public async Task<ICollection<Category>> GetCategoriesAsync()
		{
			return await _categoryrepo.GetAll().ToListAsync();
		}
	}
}

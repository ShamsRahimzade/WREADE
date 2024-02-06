
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<bool> CreateAsync(CategoryCreateVM vm, ModelStateDictionary modelstate);
        Task<CategoryCreateVM> CreatedAsync(CategoryCreateVM vm);
        Task<bool> UpdateAsync(int id, CategoryUpdateVM vm, ModelStateDictionary modelstate);
        Task<CategoryUpdateVM> UpdatedAsync(CategoryUpdateVM vm);
        Task<PaginationVM<Category>> GetAllAsync(int page = 1, int take = 10);
    }
}

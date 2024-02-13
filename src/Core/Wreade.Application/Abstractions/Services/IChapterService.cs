
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
    public interface IChapterService
    {
        Task<CreateChapterVM> CreatedAsync(CreateChapterVM vm);
        Task<bool> CreateAsync(CreateChapterVM vm,ModelStateDictionary ms);
        Task<PaginationVM<Chapter>> GetAllAsync(int id,int page = 1, int take = 5);
	}
}

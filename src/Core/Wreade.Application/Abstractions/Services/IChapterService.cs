
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
        Task<bool> DeleteAsync(int id);
        Task<UpdateChapterVM> UpdatedAsync(int id, UpdateChapterVM vm);
        Task<bool> UpdateAsync(int id, UpdateChapterVM vm, ModelStateDictionary modelstate);
        Task LikeChapter(int chapid);
        Task UnlikeChap(int chapid);
        Task<ChapterDetailVM> Detail(int id);
        Task<bool> ViewChapter(int chapterId);
	}
}

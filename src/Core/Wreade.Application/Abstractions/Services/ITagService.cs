using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface ITagService
	{
		Task<bool> CreateAsync(TagCreateVM vm, ModelStateDictionary modelstate);
		Task<TagCreateVM> CreatedAsync(TagCreateVM vm);
		Task<bool> UpdateAsync(int id, TagUpdateVM vm, ModelStateDictionary modelstate);
		Task<TagUpdateVM> UpdatedAsync(int id, TagUpdateVM vm);
		Task<PaginationVM<Tag>> GetAllAsync(int page = 1, int take = 10);
		Task<bool> DeleteAsync(int id);
		Task<TagDetailVM> DetailAsync(int id);

	}
}

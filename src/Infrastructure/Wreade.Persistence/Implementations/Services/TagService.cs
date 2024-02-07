using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
	internal class TagService : ITagService
	{
		private readonly ITagRepository _tagrepo;

		public TagService(ITagRepository tagrepo)
        {
			_tagrepo = tagrepo;
		}
        public async Task<bool> CreateAsync(TagCreateVM vm, ModelStateDictionary modelstate)
		{
			if (!modelstate.IsValid) return false;
			if (await _tagrepo.IsExistAsync(t=>t.Name==vm.Name))
			{
				modelstate.AddModelError("Name", "this tag is exist");
				return false;
			}
			Tag tag = new Tag
			{
				Name = vm.Name
			};
			await _tagrepo.AddAsync(tag);
			await _tagrepo.SaveChangeAsync();
			return true;
		   
		}

		public async Task<TagCreateVM> CreatedAsync(TagCreateVM vm)
		{
			return vm;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			if (id <= 0) throw new Exception("Id");
			Tag tag = await _tagrepo.GetByIdAsync(id);
			if (tag is null) throw new Exception("tag is not found");
			_tagrepo.Delete(tag);
			await _tagrepo.SaveChangeAsync();
			return true;
		}

		public async Task<PaginationVM<Tag>> GetAllAsync(int page = 1, int take = 10)
		{
			ICollection<Tag> tag = await _tagrepo.GetPagination(skip: (page - 1) * take, take: take).ToListAsync();

			int count = await _tagrepo.GetAll().OrderBy(c => c.CreatedAt).Include(t => t.BookTags).ThenInclude(bt => bt.Book).CountAsync();
			double totalpage = Math.Ceiling((double)count / take);
			PaginationVM<Tag> vm = new PaginationVM<Tag>
			{
				Items = tag,
				CurrentPage = page,
				TotalPage = totalpage
			};
			return vm;
		}

		public async Task<bool> UpdateAsync(int id, TagUpdateVM vm, ModelStateDictionary modelstate)
		{
			if (id <= 0) throw new Exception("BadRequest");
			if (!modelstate.IsValid) return false;
			Tag tag = await _tagrepo.GetByIdAsync(id);
			if (tag is null) throw new Exception("not found");
			if (await _tagrepo.IsExistAsync(c => c.Name == vm.Name) && await _tagrepo.IsExistAsync(c => c.Id != id))
			{
				modelstate.AddModelError("Name", "this category is exist");
				return false;
			}
			tag.Name = vm.Name;
			_tagrepo.Update(tag);
			await _tagrepo.SaveChangeAsync();
			return true;
		}

		public async Task<TagUpdateVM> UpdatedAsync(int id, TagUpdateVM vm)
		{
			if (id <= 0) throw new Exception("id");
			Tag exist = await _tagrepo.GetByIdAsync(id);
			if (exist == null) throw new Exception("not found");
			vm.Name = exist.Name.Trim();
			return vm;
		}
	}
}

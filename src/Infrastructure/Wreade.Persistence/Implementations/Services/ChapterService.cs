using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.Utilities.Extensions;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
    public class ChapterService:IChapterService
    {
        private readonly IChapterRepository _chaprepo;
        private readonly IWebHostEnvironment _env;
		private readonly IBookRepository _bookrepo;
		private readonly UserManager<AppUser> _user;

		public ChapterService(IChapterRepository chaprepo,IWebHostEnvironment env,IBookRepository bookrepo, UserManager<AppUser> user)
        {
            _chaprepo = chaprepo;
            _env = env;
			_bookrepo = bookrepo;
			_user = user;
		}
        public async Task<CreateChapterVM> CreatedAsync(CreateChapterVM vm)
        {
            //if (id <= 0) throw new Exception("this book is not fount");

            return vm;
        }
		
		public async Task<bool> CreateAsync(CreateChapterVM vm, ModelStateDictionary ms)
        {
            if (!ms.IsValid) return false;
            if (await _chaprepo.IsExistAsync(c => c.Title == vm.Title))
            {
                ms.AddModelError("Name", "This title is already exist");
                return false;
            }
            //vm.bookId = id;
            Chapter chapter = new Chapter
            {
                Title = vm.Title,
                Text = vm.Text,
                CreatedAt = DateTime.UtcNow,
                BookId = vm.bookId

            };
           //chapter= await _chaprepo.GetByExpressionAsync(c=>c.BookId==id);
            if (vm.Image is not null )
            {
                if (!vm.Image.CheckType("image/"))
                {
                    ms.AddModelError(string.Empty, "wrong type");
                    return false;
                }
                if (!vm.Image.ValidateSize(7))
                {
                    ms.AddModelError(string.Empty, "wrong size");
                    return false;
                }
                string file = await vm.Image.CreateFileAsync(_env.WebRootPath, "assets", "images");
                chapter.ChapterImage = file;
            }
            
            await _chaprepo.AddAsync(chapter);
            await _chaprepo.SaveChangeAsync();
            return true;

        }
		public async Task<PaginationVM<Chapter>> GetAllAsync(int id,int page = 1, int take = 5)
		{
			if (page < 1 || take < 1) throw new Exception("Bad request");
			ICollection<Chapter> chapter = await _chaprepo.GetPagination(skip: (page - 1) * take, take: take, orderExpression: x => x.Id, IsDescending: true, includes: nameof(Book)).Where(c=>c.BookId==id).ToListAsync();
			if (chapter == null) throw new Exception("Not Found");
			int count = await _chaprepo.GetAll().CountAsync();
			if (count < 0) throw new Exception("Not Found");
			double totalpage = Math.Ceiling((double)count / take);
			PaginationVM<Chapter> vm = new PaginationVM<Chapter>
			{
				Items = chapter,
				CurrentPage = page,
				TotalPage = totalpage
			};
			return vm;
		}


	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        private readonly IBookRepository _bookrepo;
        private readonly IWebHostEnvironment _env;

        public CategoryService(ICategoryRepository categoryrepo,IBookRepository bookrepo,IWebHostEnvironment env)
        {
            _categoryrepo = categoryrepo;
            _bookrepo = bookrepo;
            _env = env;
        }

        public async Task<bool> CreateAsync(CategoryCreateVM vm, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid) return false;
            
            if (await _categoryrepo.IsExistAsync(c=>c.Name==vm.Name))
            {
                modelstate.AddModelError("Name", "this category is exist");
                return false;
            }
            Category category = new Category
            {
                Name = vm.Name,
                BookCategories = new List<BookCategory>(),

            };
            if (vm.BookIds!=null)
            {
                foreach (var item in vm.BookIds)
                {
                    if (!(await _bookrepo.IsExistAsync(b=>b.Id==item)))
                    {
                        modelstate.AddModelError("BookIds", "books");
                        return false;
                    }
                }
                foreach (var item in vm.BookIds)
                {
                    BookCategory bookcategory = new BookCategory
                    {
                        BookId = item
                    };
                    category.BookCategories.Add(bookcategory);

                }
            }
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
            Image main = new Image
            {
                IsPrimary = true,
                URL = await vm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "images")
            };
            category.Image = main;
            await _categoryrepo.AddAsync(category);
            await _categoryrepo.SaveChangeAsync();
            return true;
        }

        public async Task<CategoryCreateVM> CreatedAsync(CategoryCreateVM vm)
        {
            vm.books = await _bookrepo.GetAll().ToListAsync();
            return vm;
        }

        public async Task<PaginationVM<Category>> GetAllAsync(int page = 1, int take = 10)
        {
            ICollection<Category> category = await _categoryrepo.GetPagination(skip: (page - 1) * take, take: take, includes: nameof(Book)).ToListAsync();
            int count = await _categoryrepo.GetAll().CountAsync();
            double totalpage = Math.Ceiling((double)count / take);
            PaginationVM<Category> vm = new PaginationVM<Category>
            {
                Items = category,
                CurrentPage = page,
                TotalPage = totalpage
            };
            return vm;
        }

        public Task<bool> UpdateAsync(int id, CategoryUpdateVM vm, ModelStateDictionary modelstate)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryUpdateVM> UpdatedAsync(CategoryUpdateVM vm)
        {
            throw new NotImplementedException();
        }
    }
}

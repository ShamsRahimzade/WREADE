using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public  class BookCreateVM
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile CoverPhoto { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<int>? CategoryIds { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<int>? TagIds { get; set; }
        public bool IsCompleted { get; set; }
    }
}

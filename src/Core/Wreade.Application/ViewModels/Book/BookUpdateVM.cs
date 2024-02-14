

using Microsoft.AspNetCore.Http;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
    public class BookUpdateVM
    {
        public string? Name { get; set; }
       
        public string? Description { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<int>? TagIds { get; set; }
        public ICollection<int>? CategoryIds { get; set; }
		public ICollection<Category>? Categories { get; set; }
        public double Rating { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsAdult { get; set; }
        public string? AppUserId { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
	public class CreateChapterVM
	{
		public string Title { get; set; }
		public string Text { get; set; }
		public IFormFile Image { get; set; }
		public int bookId { get; set; }
	}
}

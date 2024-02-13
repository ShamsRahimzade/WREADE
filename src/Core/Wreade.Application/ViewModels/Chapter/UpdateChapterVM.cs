using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wreade.Application.ViewModels
{
	public class UpdateChapterVM
	{
		public string? Title { get; set; }
		public string? Text { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wreade.Domain.Entities;

namespace Wreade.Application.ViewModels
{
    public class CategoryUpdateVM
    {
        public string? Name { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Image { get; set; }
       
      
    }
}

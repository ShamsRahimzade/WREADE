using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Wreade.Domain.Entities;


namespace Wreade.Application.ViewModels
{
    public class CategoryCreateVM
    {
        public string Name { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
       
       
    }
}

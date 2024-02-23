using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Wreade.Application.ViewModels
{
    public class EditProfileVM
    {
        [Display(Name = "Profile Picture")]
        public IFormFile? MainImageFile { get; set; }
        public string? MainImage { get; set; }
        [Display(Name = "Profile Picture")]
        public IFormFile? BackImageFile { get; set; }
        public string? BackImage { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain alphabetic characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain alphabetic characters")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; init; }
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string? CurrentPassword { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string? NewPassword { get; set; }
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [Compare(nameof(NewPassword))]
        public string? ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Date Of Birth is required.")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string? SelfInformation { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }

        public string? Twitter { get; set; }
    }
}

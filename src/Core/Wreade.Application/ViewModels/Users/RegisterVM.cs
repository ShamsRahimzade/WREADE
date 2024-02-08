
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using Wreade.Domain.Enums;

namespace Wreade.Application.ViewModels
{
	public class RegisterVM
	{
		[Required(ErrorMessage = "Ad bosh gonderile bilmez")]
		[MinLength(3, ErrorMessage = "Adin uzunlugu 3`den kichik omas")]
		[MaxLength(25, ErrorMessage = "Adin uzunlugu 25`den boyuk omas")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Soyad bosh gonderile bilmez")]
		[MinLength(3, ErrorMessage = "Soyadin uzunlugu 3`den kichik omas")]
		[MaxLength(50, ErrorMessage = "Soyadin uzunlugu 50`den boyuk omas")]
		public string Surname { get; set; }
		[Required(ErrorMessage = "Mail addressi bosh gonderile bilmez")]
		[MinLength(3, ErrorMessage = "Mail adi uzunlugu 3`den kichik omas")]
		[MaxLength(320, ErrorMessage = "Mail adi uzunlugu 320`den boyuk omas")]
		[DataType(DataType.EmailAddress)]
        public string Email { get; set; }
		[Required(ErrorMessage = "Password bosh gonderile bilmez")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Password bosh gonderile bilmez")]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "Istifadechi adi bosh gonderile bilmez")]
		[MinLength(8, ErrorMessage = "Username uzunlugu 8`den kichik omas")]
		[MaxLength(100, ErrorMessage = "Adin uzunlugu 100`den boyuk omas")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Birthday bosh gonderile bilmez")]
		[DataType(DataType.DateTime)]
		public DateTime BirthDay { get; set; }
		public IFormFile? MainImage { get; set; }
		public IFormFile? BackImage { get; set; }
        [Required(ErrorMessage = "Please select a role.")]
      
        public UserRole Role { get; set; }


    }
}

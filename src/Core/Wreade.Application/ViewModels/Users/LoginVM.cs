using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wreade.Application.ViewModels
{
	public class LoginVM
	{
		[Required]
		[MinLength(3)]
		[MaxLength(320)]
		public string UserNameOrEmail { get; set; }
		[Required]
		[MinLength(8)]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool IsRemembered { get; set; }
	}
}

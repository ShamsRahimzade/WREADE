using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Wreade.Application.ViewModels;

namespace Wreade.Application.Validators
{
	public class LoginVMValidator : AbstractValidator<LoginVM>
	{
        public LoginVMValidator()
        {
			RuleFor(x => x.UserNameOrEmail)
			   .NotEmpty()
			   .MaximumLength(256);
		}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using Wreade.Application.ViewModels;

namespace Wreade.Application.Validators
{
	internal class RegisterVMValidator : AbstractValidator<RegisterVM>
	{
        public RegisterVMValidator()
        {
			RuleFor(x => x.Email)
			   .NotEmpty()
			   .Must(IsValidEmail).WithMessage("mail sefdiiiii")
			   .MaximumLength(256).WithMessage("uzunduuuu");
			RuleFor(x => x.Password)
				.NotEmpty()
				.MinimumLength(8).WithMessage("qisadiiii")
				.MaximumLength(100).WithMessage("uzunduuuu")
				.Must(Password).WithMessage("shifrede en az 1 boyuk herf ve reqem olmalidir");
			RuleFor(x => x.UserName)
			   .NotEmpty()
			   .MinimumLength(6).WithMessage("qisadiiii")
			   .MaximumLength(256).WithMessage("uzunduuuu");
			RuleFor(x => x.Name)
			   .NotEmpty()
			   .MinimumLength(3).WithMessage("qisadiiii")
			   .MaximumLength(25).WithMessage("uzunduuuu");
			RuleFor(x => x.Surname)
			   .NotEmpty()
			   .MinimumLength(3).WithMessage("qisadiiii")
			   .MaximumLength(100).WithMessage("uzunduuuu");
			RuleFor(x => x).Must(x => x.ConfirmPassword == x.Password);
		}
		public bool IsValidEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

			Regex regex = new Regex(pattern);

			return regex.IsMatch(email);
		}
		public bool Password(string password)
		{
			bool digit = false;
			bool lower = false;
			bool upper = false;
			foreach (char item in password)
			{
				if (char.IsDigit(item))
				{
					digit = true;
				}
				if (char.IsLower(item))
				{
					lower = true;
				}
				if (char.IsUpper(item))
				{
					upper = true;
				}
			}
			return digit && lower && upper;
		}
    }
}

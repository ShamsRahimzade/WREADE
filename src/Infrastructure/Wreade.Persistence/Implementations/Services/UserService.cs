
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.Utilities.Extensions;
using Wreade.Application.ViewModels;


using Wreade.Domain.Entities;
using Wreade.Domain.Enums;

namespace Wreade.Persistence.Implementations.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _user;
		private readonly IMapper _mapper;
		private readonly SignInManager<AppUser> _signman;
		private readonly RoleManager<IdentityRole> _roleman;
		private readonly IWebHostEnvironment _env;

		public UserService(UserManager<AppUser> user, IMapper mapper, SignInManager<AppUser> signman, RoleManager<IdentityRole> roleman, IWebHostEnvironment env)
		{
			_user = user;
			_mapper = mapper;
			_signman = signman;
			_roleman = roleman;
			_env = env;
		}

		public async Task CreateRoleAsync()
		{
			foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
			{
				if (!await _roleman.RoleExistsAsync(role.ToString()))
				{
					await _roleman.CreateAsync(new IdentityRole
					{
						Name = role.ToString(),
					});

				}
			}
		}

		public async Task<bool> Login(LoginVM login, ModelStateDictionary modelstate)
		{
			if (!modelstate.IsValid) return false;
			AppUser user = await _user.FindByEmailAsync(login.UserNameOrEmail);
			if (user == null)
			{
				user = await _user.FindByNameAsync(login.UserNameOrEmail);
				if (user == null)
				{
					modelstate.AddModelError(string.Empty,"user empty");
					return false;

				}
			}
			var result = await _signman.PasswordSignInAsync(user, login.Password, login.IsRemembered, true);
			if (result.IsLockedOut)
			{
				modelstate.AddModelError(string.Empty, "bloked");
				return false;
			}
			if (!result.Succeeded)
			{
				modelstate.AddModelError(string.Empty, "not succeeded");
				return false;
			}
			return true;
		}

		public async Task Logout()
		{
			await _signman.SignOutAsync();

		}

		public async Task<bool> Register(RegisterVM register, ModelStateDictionary modelstate)
		{
			if (!modelstate.IsValid) return false;
			if (!register.Name.IsLetter())
			{
				modelstate.AddModelError("Name", "letter");
				return false;
			}
			if (!register.Email.CheeckEmail())
			{
				modelstate.AddModelError("Name", "mail");
				return false;
			}
			register.Name.Capitalize();
			register.Surname.Capitalize();
			AppUser user = new AppUser
			{
				Name = register.Name,
				UserName = register.UserName,
				Surname = register.Surname,
				Email = register.Email,
				Birthday = register.BirthDay,

			};

			if (register.MainImage is not null)
			{
				if (!register.MainImage.CheckType("image/"))
				{
					modelstate.AddModelError("MainImage", "wrong image type");
					return false;
				}
				if (!register.MainImage.ValidateSize(7))
				{
					modelstate.AddModelError("MainImage", "wrong image size");
					return false;
				}
				user.MainImage = await register.MainImage.CreateFileAsync(_env.WebRootPath, "assets", "images");
			}
            if (register.BackImage is not null)
            {
                if (!register.BackImage.CheckType("image/"))
                {
					modelstate.AddModelError("BackImage", "wrong image type");
					return false;
				}
                if (!register.BackImage.ValidateSize(7))
                {
					modelstate.AddModelError("BackImage", "wrong image size");
					return false;
				}
                user.BackImage = await register.BackImage.CreateFileAsync(_env.WebRootPath, "assets", "images");
            }
            IdentityResult result = await _user.CreateAsync(user, register.Password);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{

					modelstate.AddModelError(string.Empty, error.Description);
					
				}
				return false;
			}

            await _signman.SignInAsync(user, isPersistent: false);
            if (user != null)
            {
                await AssignRoleToUser(user, register.SelectedRole);
            }
			return true;

		}
        public async Task AssignRoleToUser(AppUser user, string roleName)
        {

            if (!await _roleman.RoleExistsAsync(roleName))
            {

                await _roleman.CreateAsync(new IdentityRole
                {
                    Name = roleName
                });
            }
            await _user.AddToRoleAsync(user, roleName);
        }
        public async Task CreateAdminRoleAsync()
        {
            var adminRoleName = "Admin";

            if (!await _roleman.RoleExistsAsync(adminRoleName))
            {
                await _roleman.CreateAsync(new IdentityRole
                {
                    Name = adminRoleName,
                });
            }
        }
    }
}

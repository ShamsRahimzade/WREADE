
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

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

		public async Task<List<string>> Login(LoginVM login)
		{
			List<string> String = new List<string>();
			AppUser user = await _user.FindByEmailAsync(login.UserNameOrEmail);
			if (user == null)
			{
				user = await _user.FindByNameAsync(login.UserNameOrEmail);
				if (user == null)
				{
					String.Add("Username, Email or Password was wrong");
					return String;

				}
			}
			var result = await _signman.PasswordSignInAsync(user, login.Password, login.IsRemembered, true);
			if (result.IsLockedOut)
			{
				String.Add("You have a lot of fail  try that is why you banned please try some minuts late");
				return String;
			}
			if (!result.Succeeded)
			{
				String.Add("Username, Email or Password was wrong");
				return String;
			}
			return String;
		}

		public async Task Logout()
		{
			await _signman.SignOutAsync();

		}

		public async Task<List<string>> Register(RegisterVM register)
		{
			List<string> String = new List<string>();
			if (!register.Name.IsLetter())
			{
				String.Add("Your Name or Surname only contain letters");
				return String;
			}
			if (!register.Email.CheeckEmail())
			{
				String.Add("Your Email type is not true");
				return String;
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
					String.Add("Your photo type is not true.Please use only image");
					return String;
				}
				if (!register.MainImage.ValidateSize(7))
				{
					String.Add("Your Email size must be max 7 mb");
					return String;
				}
				user.MainImage = await register.MainImage.CreateFileAsync(_env.WebRootPath, "assets", "images");
			}
            if (register.BackImage is not null)
            {
                if (!register.BackImage.CheckType("image/"))
                {
                    String.Add("Your photo type is not true.Please use only image");
                    return String;
                }
                if (!register.BackImage.ValidateSize(7))
                {
                    String.Add("Your Email size must be max 5mb");
                    return String;
                }
                user.BackImage = await register.BackImage.CreateFileAsync(_env.WebRootPath, "assets", "images");
            }
            IdentityResult result = await _user.CreateAsync(user, register.Password);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{

					String.Add(error.Description);
				}
				return String;
			}

            await _signman.SignInAsync(user, isPersistent: false);
            if (user != null)
            {
                await AssignRoleToUser(user, register.SelectedRole);
            }
			return String;

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

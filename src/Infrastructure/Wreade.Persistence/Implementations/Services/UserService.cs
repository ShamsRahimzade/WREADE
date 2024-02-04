
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Wreade.Application.Abstractions.Services;
using Wreade.Application.ViewModels;

using Wreade.Application.ViewModels.Users;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.Implementations.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _user;
		private readonly IMapper _mapper;
		private readonly SignInManager<AppUser> _signman;

		public UserService(UserManager<AppUser> user, IMapper mapper,SignInManager<AppUser> signman)
        {
			_user = user;
			_mapper = mapper;
			_signman = signman;
		}
        public async Task Login(LoginVM loginVM)
		{
			AppUser user = await _user.FindByNameAsync(loginVM.UserNameOrEmail);
			if (user is null)
			{
				user = await _user.FindByEmailAsync(loginVM.UserNameOrEmail);
				if (user is null) throw new Exception("tapilmiyoorkeee");
			}
			if (!await _user.CheckPasswordAsync(user, loginVM.Password)) throw new Exception("tapilmiyoorkeee");
		
		}

		public async Task Register(RegisterVM registerVM)
		{
			if (await _user.Users.AnyAsync(x => x.UserName == registerVM.UserName || x.Email == registerVM.Email)) throw new Exception("Eyni ad omass");
			AppUser appUser = _mapper.Map<AppUser>(registerVM);
			var result = await _user.CreateAsync(appUser, registerVM.Password);
			if (!result.Succeeded)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (var item in result.Errors)
				{
					stringBuilder.AppendLine(item.Description);
				}
				throw new Exception(stringBuilder.ToString());
			}
		}
		public async Task Logout()
		{
			await _signman.SignOutAsync();
		
		}
	}
}

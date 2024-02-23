
using System;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Application.Abstractions.Services;
using Wreade.Application.Utilities.Extensions;
using Wreade.Application.ViewModels;
using Wreade.Application.ViewModels.Users;
using Wreade.Domain.Entities;
using Wreade.Domain.Enums;
using Wreade.Persistence.DAL;
using static System.Net.WebRequestMethods;

namespace Wreade.Persistence.Implementations.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _user;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signman;
        private readonly RoleManager<IdentityRole> _roleman;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _http;
        private readonly IFollowRepository _followRepo;
		private readonly AppDbContext _context;

		public UserService(
         UserManager<AppUser> user,
         IMapper mapper,
         SignInManager<AppUser> signman,
         RoleManager<IdentityRole> roleman,
         IWebHostEnvironment env,
         IHttpContextAccessor http,
         IFollowRepository followRepo,
         AppDbContext context
      
         )
        {
            _user = user;
            _mapper = mapper;
            _signman = signman;
            _roleman = roleman;
            _env = env;
            _http = http;
            _followRepo = followRepo;
			_context = context;
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

		public async Task<bool> Login(LoginVM vm, ModelStateDictionary modelstate)
        {
            if (!modelstate.IsValid) return false;
            AppUser user = await _user.FindByEmailAsync(vm.UserNameOrEmail);
            if (user is null)
            {
                user = await _user.FindByNameAsync(vm.UserNameOrEmail);
                if (user is null)
                {
                    modelstate.AddModelError(string.Empty, "Not found");
                    return false;
                } 
            }
            var result = await _signman.PasswordSignInAsync(user, vm.Password, vm.IsRemembered, true);
            if (result.IsLockedOut)
            {
                modelstate.AddModelError(string.Empty, "Account is locked. Please try again after a few minutes.");
                return false;
            }
            if (!result.Succeeded)
            {
                modelstate.AddModelError(string.Empty, "Password or email incorrect");
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
                user.MainImage = await register.MainImage.CreateFileAsync(_env.WebRootPath, "assets", "assets", "img");
            }
			if (!ValidateRole(register.Role, modelstate)) return false;
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
                user.BackImage = await register.BackImage.CreateFileAsync(_env.WebRootPath, "assets", "assets", "img");
            }
            var result = await _user.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {

                    modelstate.AddModelError(string.Empty, error.Description);

                }
                return false;
            }

            if (user != null)
            {
                await AssignRoleToUser(user, register.Role.ToString());
            }
            await _signman.SignInAsync(user, isPersistent: false);
            return true;

        }
		private bool ValidateRole(UserRole role, ModelStateDictionary modelstate)
		{
			if (!Enum.IsDefined(typeof(UserRole), role))
			{
				modelstate.AddModelError(string.Empty, "Please select a valid role");
				return false;
			}
			return true;
		}
		public async Task<List<string>> UpdateUser(AppUser user, EditProfileVM vm)
        {
            List<string> str = new List<string>();

            user.Name = vm.Name.Capitalize();
            user.Surname = vm.Surname.Capitalize();
            user.Birthday = vm.BirthDate;
            user.UserName = vm.Username;
            user.MainImage = vm.MainImage;
            user.BackImage = vm.BackImage;
            user.SelfInformation = vm.SelfInformation;
            user.Instagram = vm.Instagram;
            user.Facebook = vm.Facebook;
            user.Twitter = vm.Twitter;
            if (user.Email != vm.Email)
            {
                var eres = await _user.SetEmailAsync(user, vm.Email);
                if (!eres.Succeeded)
                {
                    foreach (var item in eres.Errors)
                    {
                        str.Add(item.Description);
                    }
                    return str;
                }
            }
            if (vm.NewPassword is not null)
            {
                var pres = await _user.ChangePasswordAsync(user, vm.CurrentPassword, vm.NewPassword);
                if (!pres.Succeeded)
                {
                    foreach (var item in pres.Errors)
                    {
                        str.Add(item.Description);
                    }
                    return str;
                }
            }


            if (vm.MainImageFile is not null)
            {
                if (!vm.MainImageFile.CheckType("image"))
                {
                    str.Add("Only images allowed");
                    return str;
                }
                if (vm.MainImageFile.ValidateSize(2))
                {
                    str.Add("Max file size is 7Mb");
                    return str;
                }
                user.MainImage = await vm.MainImageFile.CreateFileAsync(_env.WebRootPath, "assets", "assets", "img");
            }
            if (vm.BackImageFile is not null)
            {
                if (!vm.BackImageFile.CheckType("image"))
                {
                    str.Add("Only images allowed");
                    return str;
                }
                if (vm.BackImageFile.ValidateSize(2))
                {
                    str.Add("Max file size is 7Mb");
                    return str;
                }
                user.MainImage = await vm.BackImageFile.CreateFileAsync(_env.WebRootPath, "assets", "assets", "img");
            }

            var res = await _user.UpdateAsync(user);
            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    str.Add(item.Description);
                }
                return str;
            }
            return new List<string>();
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
        public async Task<AppUser> GetUser(string username, params Expression<Func<AppUser, object>>[] includes)
        {
			var query = _user.Users.AsQueryable();

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.FirstOrDefaultAsync(u => u.UserName == username);
			
        }
       
        public async Task Follow(string followedId)
        {
            string userId = _http.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            AppUser user = await _user.FindByIdAsync(userId);
            AppUser followed = await _user.FindByIdAsync(followedId);

            followed.FollowerCount++;
            user.FollowingCount++;

            Follow follow = new Follow
            {

                FolloweeId = followedId,
                FollowerId = userId
            };

            await _followRepo.AddAsync(follow);
            await _followRepo.SaveChangeAsync();
        }
        public async Task Unfollow(string followedId)
        {
            string userId = _http.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            AppUser user = await _user.FindByIdAsync(userId);
            AppUser followed = await _user.FindByIdAsync(followedId);

            followed.FollowerCount--;
            user.FollowingCount--;

            Follow foll = await _followRepo.GetByExpressionAsync(f => f.FolloweeId == followedId && f.FollowerId == userId);

            if (foll != null)
            {
                _followRepo.Delete(foll);
                await _followRepo.SaveChangeAsync();
            }
        }
        public async Task<List<string>> LoginNoPass(string username)
        {


            List<string> str = new List<string>();
            AppUser user = await _user.FindByEmailAsync(username);
            if (user is null)
            {
                user = await _user.FindByNameAsync(username);
                if (user is null)
                {
                    str.Add("Username email or password is wrong!");
                    return str;
                }

            }
            await _signman.SignInAsync(user, true);

            return new List<string>();
        }
       

		public async Task<bool> UpgradeToPremiumAsync(string userId)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user != null)
			{
				user.IsPremium = true;
				user.PremiumStartDate = DateTime.Now;
				user.PremiumEndDate = DateTime.Now.AddMonths(1); 
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<bool> DowngradeFromPremiumAsync(string userId)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user != null)
			{
				user.IsPremium = false;
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<AppUser> GetUserById(string userId, params Expression<Func<AppUser, object>>[] includes)
		{
			var query = _user.Users.AsQueryable();

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.FirstOrDefaultAsync(u => u.Id == userId);
			
		}
        
	}
}

using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface IUserService
	{
		Task<bool> Register(RegisterVM register, ModelStateDictionary modelstate);
		Task<bool> Login(LoginVM vm, ModelStateDictionary modelstate);
		Task Logout();
		Task CreateRoleAsync();
        Task CreateAdminRoleAsync();
        Task AssignRoleToUser(AppUser user, string roleName);
		Task<List<string>> UpdateUser(AppUser user, EditProfileVM vm);
		Task<AppUser> GetUser(string username, params Expression<Func<AppUser, object>>[] includes);
		
		Task Follow(string followedId);
		Task Unfollow(string followedId);
        Task<List<string>> LoginNoPass(string username);
		Task<AppUser> GetUserById(string userId, params Expression<Func<AppUser, object>>[] includes);
		Task<bool> UpgradeToPremiumAsync(string userId);
		Task<bool> DowngradeFromPremiumAsync(string userId);

	}
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface IUserService
	{
		Task<bool> Register(RegisterVM register, ModelStateDictionary modelstate);
		Task<bool> Login(LoginVM login, ModelStateDictionary modelstate);
		Task Logout();
		Task CreateRoleAsync();
        Task CreateAdminRoleAsync();
        Task AssignRoleToUser(AppUser user, string roleName);
		Task<List<string>> UpdateUser(AppUser user, EditProfileVM vm);
		Task<AppUser> GetUser(string username);
		Task<List<AppUser>> GetUsers(string searchTerm);
		Task Follow(string followedId);
		Task Unfollow(string followedId);
        Task<List<string>> LoginNoPass(string username);
		Task<AppUser> GetUserById(string userId);
		//Task<bool> ResetPassword(ResetPasswordVM vm, string userId, string token, ModelStateDictionary ms);
		//Task<bool> resetPassword(string userId, string token);
		//Task<bool> ForgotPassword(ForgotPasswordVM vm, ModelStateDictionary ms);
    }
}

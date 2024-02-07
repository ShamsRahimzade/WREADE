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
    }
}

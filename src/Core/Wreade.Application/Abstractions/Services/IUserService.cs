
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.Abstractions.Services
{
	public interface IUserService
	{
		Task<List<string>> Register(RegisterVM register);
		Task<List<string>> Login(LoginVM login);
		Task Logout();
		Task CreateRoleAsync();
        Task CreateAdminRoleAsync();
        Task AssignRoleToUser(AppUser user, string roleName);
    }
}

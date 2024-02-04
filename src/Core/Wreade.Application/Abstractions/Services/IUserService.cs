using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wreade.Application.ViewModels;
using Wreade.Application.ViewModels.Users;

namespace Wreade.Application.Abstractions.Services
{
	public interface IUserService
	{
		Task Register(RegisterVM registerVM);
		Task Login(LoginVM loginVM);
		Task Logout();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Wreade.Application.ViewModels;
using Wreade.Domain.Entities;

namespace Wreade.Application.MappingProfiles
{
	public class AppUserProfile:Profile
	{
        public AppUserProfile()
        {
			CreateMap<RegisterVM, AppUser>();
            CreateMap<AppUser, EditProfileVM>().ReverseMap();

        }
    }
}

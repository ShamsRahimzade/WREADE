
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Wreade.Persistence.DAL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Wreade.Domain.Entities;
using Wreade.Application.Abstractions.Services;
using Wreade.Persistence.Implementations.Services;
using Wreade.Application.Abstractions.Repostories;
using Wreade.Persistence.Implementations.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Wreade.Application.MappingProfiles;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Wreade.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("default")));
			services.AddAutoMapper(typeof(AppUserProfile));
            services.AddAuthentication();
           
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();


            //services.AddScoped<IActionContextAccessor, ActionContextAccessor>();
            //services.AddSingleton<IUrlHelper, UrlHelper>();
            //services.AddHttpContextAccessor();
            services.AddScoped<IHomeService, HomeService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();
      




            return services;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WEB.Models;
using Wreade.Persistence.DAL;

namespace Wreade.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("default")));
            //services.AddAutoMapper(typeof(TagProfile));
            //services.AddAutoMapper(typeof(CategoryProfile));
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();

            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductService, ProductService>();

            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<ICategoryService, CategoryService>();

            //services.AddScoped<ITagRepository, TagRepository>();
            //services.AddScoped<ITagService, TagService>();

            //services.AddScoped<IColorRepository, ColorRepository>();

            //services.AddScoped<IAuthService, AuthService>();



            return services;
        }
    }
}

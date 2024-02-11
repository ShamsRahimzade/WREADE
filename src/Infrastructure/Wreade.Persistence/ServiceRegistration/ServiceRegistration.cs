
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

using Wreade.Application.MappingProfiles;

using Wreade.Application.Abstractions.Services.MailService;

namespace Wreade.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("default")));
			services.AddAutoMapper(typeof(AppUserProfile));
            services.AddAuthentication();
			services.AddScoped<IMailService, MailService>();
			services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;
				opt.SignIn.RequireConfirmedAccount = true;
			}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();
            //services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));
           
            //services.AddScoped<IActionContextAccessor, ActionContextAccessor>();
            //services.AddSingleton<IUrlHelper, UrlHelper>();
            //services.AddHttpContextAccessor();
            services.AddScoped<IHomeService, HomeService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IFollowRepository, FollowRepository>();
			services.AddScoped<IChapterViewCountRepository, ChapterViewCountRepository>();
			services.AddScoped<IChapterRepository, ChapterRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();

	//		services
	//.AddAuthentication(options =>
	//{
	//	options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
	//	options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
	//	options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
	//});



			return services;
        }
    }
}

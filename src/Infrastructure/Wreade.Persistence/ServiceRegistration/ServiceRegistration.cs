﻿
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
			}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

			services.AddTransient<List<Book>>();
			services.AddScoped<IHomeService, HomeService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<IChapterService, ChapterService>();
			services.AddScoped<IChapterRepository, ChapterRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IFollowRepository, FollowRepository>();
			services.AddScoped<IChapterRepository, ChapterRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ISearchService, SearchService>();
			services.AddScoped<ISettingsRepository, SettingsRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>(); 
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
			services.AddScoped< LayoutService>();
			





			return services;
        }
    }
}

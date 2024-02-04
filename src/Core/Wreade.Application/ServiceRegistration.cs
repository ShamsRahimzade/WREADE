using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using FluentValidation;
using Wreade.Application.MappingProfiles;

namespace Wreade.Application
{

    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppUserProfile));
            //services.AddAutoMapper(typeof(CategoryProfile));


            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Wreade.Application
{

    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(TagProfile));
            //services.AddAutoMapper(typeof(CategoryProfile));


            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services
            //    .AddFluentValidationAutoValidation()
            //    .AddFluentValidationClientsideAdapters()
            //    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

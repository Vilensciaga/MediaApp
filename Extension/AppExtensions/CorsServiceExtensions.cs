using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions.AppExtensions
{
    public static class CorsServiceExtensions
    {
        public static IServiceCollection CorsServices(this IServiceCollection services)
        {
            //adding cors policy
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:4200");
                });
            });

            return services;
        }
    }
}

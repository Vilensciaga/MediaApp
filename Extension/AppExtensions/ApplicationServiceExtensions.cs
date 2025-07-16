using Database.Data;
using Database.Interface;
using DataService.Interface;
using DataService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Dtos.User;
using Auth.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models.Settings;

namespace Extensions.AppExtensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            // adding the database context and connection string
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
                config.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Database")));

            //dependency injections
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<IUserRepository, UserRepository>();


            //database context for dependency injection
            services.AddTransient<IAppDbContext, AppDbContext>();




            //instanciating mapping profiles
            services.AddAutoMapper(typeof(UserMappingProfile));

            //configuring our cloudinary settings
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

            services.AddTransient<IPhotoService, PhotoService>();


            return services;
        }
    }
}

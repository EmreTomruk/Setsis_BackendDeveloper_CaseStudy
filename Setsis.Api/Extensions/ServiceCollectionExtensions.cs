using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Setsis.Core.Models;
using Setsis.Core.Repositories;
using Setsis.Core.UnitOfWork;
using Setsis.Core.Validation;
using Setsis.Infrastructure;
using Setsis.Infrastructure.Repositories;
using Setsis.Infrastructure.UnitOfWork;
using Setsis.Service.Category;
using Setsis.Service.Mapping;
using Setsis.Service.Services.Authenticate;
using Setsis.Service.Services.Category;
using Setsis.Service.Services.Token;
using Setsis.Service.Services.User;

namespace Setsis.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMappers(this IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(typeof(IMapper), _ => mapper);

            return services;
        }

        public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SetsisDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SetsisDbConnection")));

            return services;
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SetsisDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddTransient<ITokenService, TokenService>();            

            return services;
        }
    }
}

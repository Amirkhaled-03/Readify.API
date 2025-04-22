using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Readify.API.Filters;
using Readify.BLL;
using Readify.DAL;
using Readify.DAL.Configuration;
using Readify.DAL.DBContext;
using Readify.DAL.Entities.Identity;
using Swashbuckle.AspNetCore.Filters;

namespace Readify.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // user settings
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\`~؀-ۿ";

                // Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero; // Disable automatic reset
                options.Lockout.MaxFailedAccessAttempts = int.MaxValue; // Prevent automatic resets
                options.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            services.AddScoped<UserManager<ApplicationUser>, CustomUserManager>();
            services.AddScoped<RoleManager<ApplicationRole>, CustomRoleManager>();

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityServices(configuration);
            services.RegisterDataAccessServices();
            services.RegisterBusinessLogicServices();
            services.AddHttpContextAccessor();
            return services;
        }

        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelStateFilter>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedApps", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerDocumentation();

            // Register Swagger example providers
            services.AddSwaggerExamplesFromAssemblyOf<Program>();




            return services;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace IdentityJWT.Extensions
{
    public static class IdentityConfigurationExtension
    {
        public static IServiceCollection ConfigureIdentiyCustom(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 3;

                option.User.RequireUniqueEmail = false;

                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
                option.Lockout.MaxFailedAccessAttempts = 3;
            });
            return services;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Business.Db
{
    public static class Setup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
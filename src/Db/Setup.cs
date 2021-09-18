namespace Accounts.Business.Db
{
    using Microsoft.Extensions.DependencyInjection;

    public static class Setup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
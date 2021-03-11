using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Business.Mappers
{
    public static class Setup
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IAccountMapper, AccountMapper>();

            return services;
        }
    }
}
namespace Accounts.Business.Mappers
{
    using Microsoft.Extensions.DependencyInjection;

    public static class Setup
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddTransient<IAccountMapper, AccountMapper>();

            return services;
        }
    }
}
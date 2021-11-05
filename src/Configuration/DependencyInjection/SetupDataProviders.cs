namespace Configuration.DependencyInjection
{
    using System;
    using AlbedoTeam.Sdk.DataLayerAccess;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using Core.UseCases.InterfaceAdapters;
    using Dataprovider.Db.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupDataProviders
    {
        public static IServiceCollection AddDataProviders(
            this IServiceCollection services,
            Action<IDbSettings> configureDb)
        {
            return services
                .AddRepositories(configureDb)
                .AddServices();
        }

        private static IServiceCollection AddRepositories(
            this IServiceCollection services,
            Action<IDbSettings> configureDb)
        {
            return services
                .AddDataLayerAccess(configureDb)
                .AddScoped<IAccountRepository, AccountRepository>();
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
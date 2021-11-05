namespace Accounts.Configuration
{
    using System;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using AutoMapper;
    using DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupAplication
    {
        public static IServiceCollection AddConfiguration(
            this IServiceCollection services,
            Action<IDbSettings> configureDb,
            Action<IMapperConfigurationExpression> configureMappers = null)
        {
            return services
                .AddMappers(configureMappers)
                .AddUseCases()
                .AddDataProviders(configureDb);
        }
    }
}
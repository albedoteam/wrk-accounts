namespace Configuration.DependencyInjection
{
    using System;
    using AutoMapper;
    using Core.UseCases.Mappers;
    using Dataprovider.Db.Mappers;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupMappers
    {
        public static IServiceCollection AddMappers(
            this IServiceCollection services,
            Action<IMapperConfigurationExpression> configure = null)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CoreUseCasesProfile());
                mc.AddProfile(new DataProviderDbProfile());

                configure?.Invoke(mc);
            });

            var mapper = mapperConfig.CreateMapper();

            return services.AddSingleton(mapper);
        }
    }
}
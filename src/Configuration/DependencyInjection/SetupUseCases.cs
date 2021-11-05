namespace Configuration.DependencyInjection
{
    using System;
    using System.Reflection;
    using Albedo.Sdk.UseCases;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class SetupUseCases
    {
        private const string UseCasesAssemblyName = "Core.UseCases";
        private static readonly Assembly AssemblyName = AppDomain.CurrentDomain.Load(UseCasesAssemblyName);

        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            return services
                .AddHandlers()
                .AddValidators();
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            return services
                .AddPipelineBehaviors()
                .AddMediatR(AssemblyName);
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            AssemblyScanner.FindValidatorsInAssembly(AssemblyName)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            return services;
        }
    }
}
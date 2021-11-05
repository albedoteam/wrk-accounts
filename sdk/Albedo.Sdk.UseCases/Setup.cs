namespace Albedo.Sdk.UseCases
{
    using FailFast;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class Setup
    {
        public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
        }
    }
}
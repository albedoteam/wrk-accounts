namespace Accounts.Business
{
    using Configuration;
    using Mappers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfiguration(
                db =>
                {
                    db.ConnectionString = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
                    db.DatabaseName = Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
                },
                mapper => mapper.AddProfile(new EntrypointGrpcProfile()));

            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<AccountService>();

                endpoints.MapGet("/",
                    context => context.Response.WriteAsync("gRPC Service"));
            });
        }
    }
}
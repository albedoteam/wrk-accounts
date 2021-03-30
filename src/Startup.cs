namespace Accounts.Business
{
    using AlbedoTeam.Accounts.Contracts.Events;
    using AlbedoTeam.Sdk.DataLayerAccess;
    using AlbedoTeam.Sdk.JobWorker.Configuration.Abstractions;
    using AlbedoTeam.Sdk.MessageConsumer;
    using Consumers;
    using Db;
    using Mappers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup : IWorkerConfigurator
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayerAccess(db =>
            {
                db.ConnectionString = configuration.GetValue<string>("DatabaseSettings_ConnectionString");
                db.DatabaseName = configuration.GetValue<string>("DatabaseSettings_DatabaseName");
            });

            services.AddMappers();
            services.AddRepositories();
            services.AddTransient<IJobRunner, JobConsumer>();

            services.AddBroker(
                configure => configure
                    .SetBrokerOptions(broker => broker.Host = configuration.GetValue<string>("Broker_Host")),
                consumers => consumers
                    .Add<ListAccountsConsumer>()
                    .Add<GetAccountConsumer>()
                    .Add<CreateAccountConsumer>()
                    .Add<UpdateAccountConsumer>()
                    .Add<DeleteAccountConsumer>(),
                queues => queues
                    .Map<AccountUpdated>()
                    .Map<AccountDeleted>());
        }
    }
}
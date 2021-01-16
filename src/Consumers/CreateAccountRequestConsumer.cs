using System;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class CreateAccountRequestConsumer : IConsumer<CreateAccountRequest>
    {
        public Task Consume(ConsumeContext<CreateAccountRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class GetAccountRequestConsumer : IConsumer<GetAccountRequest>
    {
        public Task Consume(ConsumeContext<GetAccountRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
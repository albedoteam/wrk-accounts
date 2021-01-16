using System;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class ListAccountsRequestConsumer : IConsumer<ListAccountsRequest>
    {
        public Task Consume(ConsumeContext<ListAccountsRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
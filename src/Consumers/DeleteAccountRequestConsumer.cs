using System;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class DeleteAccountRequestConsumer : IConsumer<DeleteAccountRequest>
    {
        public Task Consume(ConsumeContext<DeleteAccountRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
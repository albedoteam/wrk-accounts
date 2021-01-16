using System;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class UpdateAccountRequestConsumer : IConsumer<UpdateAccountRequest>
    {
        public Task Consume(ConsumeContext<UpdateAccountRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
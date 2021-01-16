using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class DeleteAccountRequestConsumer : IConsumer<DeleteAccountRequest>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public DeleteAccountRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<DeleteAccountRequest> context)
        {
            var account = await _repository.FindById(context.Message.Id);
            if (account is null)
            {
                await context.RespondAsync<AccountNotFound>(new { });
            }
            else
            {
                await _repository.DeleteById(context.Message.Id);

                // get "soft-deleted" account
                account = await _repository.FindById(context.Message.Id, true);

                await context.Publish(_mapper.MapModelToDeletedEvent(account)); // notifies 
                await context.RespondAsync(_mapper.MapModelToDeletedEvent(account)); // respond async
            }
        }
    }
}
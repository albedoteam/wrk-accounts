using System.Threading.Tasks;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using AlbedoTeam.Accounts.Business.Models;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using MassTransit;
using MongoDB.Driver;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class UpdateAccountRequestConsumer : IConsumer<UpdateAccount>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public UpdateAccountRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<UpdateAccount> context)
        {
            var account = await _repository.FindById(context.Message.Id);
            if (account is null)
            {
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = $"Not found for id {context.Message.Id}"
                });

                return;
            }

            var update = Builders<Account>.Update.Combine(
                Builders<Account>.Update.Set(a => a.Name, context.Message.Name),
                Builders<Account>.Update.Set(a => a.Description, context.Message.Description),
                Builders<Account>.Update.Set(a => a.IdentificationNumber, context.Message.IdentificationNumber),
                Builders<Account>.Update.Set(a => a.Enabled, context.Message.Enabled));

            await _repository.UpdateById(context.Message.Id, update);

            // get "updated" account
            account = await _repository.FindById(context.Message.Id);

            var updatedEvent = _mapper.MapModelToUpdatedEvent(account);

            await context.Publish(updatedEvent);
            await context.RespondAsync(updatedEvent);
        }
    }
}
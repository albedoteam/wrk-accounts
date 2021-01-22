﻿using System.Threading.Tasks;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class DeleteAccountRequestConsumer : IConsumer<DeleteAccount>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public DeleteAccountRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<DeleteAccount> context)
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

            await _repository.DeleteById(context.Message.Id);

            // get "soft-deleted" account
            account = await _repository.FindById(context.Message.Id, true);

            await context.Publish(_mapper.MapModelToDeletedEvent(account)); // notifies 
            await context.RespondAsync(_mapper.MapModelToDeletedEvent(account)); // respond async
        }
    }
}
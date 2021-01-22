using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class GetAccountRequestConsumer : IConsumer<GetAccountRequest>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public GetAccountRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<GetAccountRequest> context)
        {
            var account = await _repository.FindById(context.Message.Id, context.Message.ShowDeleted);

            if (account is null)
                await context.RespondAsync<AccountNotFound>(new { });
            else
                await context.RespondAsync(_mapper.MapModelToResponse(account));
        }
    }
}
using System.Threading.Tasks;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class GetAccountRequestConsumer : IConsumer<GetAccount>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public GetAccountRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<GetAccount> context)
        {
            var account = await _repository.FindById(context.Message.Id, context.Message.ShowDeleted);

            if (account is null)
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = $"Not found for id {context.Message.Id}"
                });
            else
                await context.RespondAsync(_mapper.MapModelToResponse(account));
        }
    }
}
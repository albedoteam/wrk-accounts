namespace Accounts.Business.Consumers
{
    using System.Threading.Tasks;
    using AlbedoTeam.Accounts.Contracts.Common;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using Db;
    using Mappers;
    using MassTransit;

    public class GetAccountConsumer : IConsumer<GetAccount>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public GetAccountConsumer(IAccountRepository repository, IAccountMapper mapper)
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
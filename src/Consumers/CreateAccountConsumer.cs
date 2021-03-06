namespace Accounts.Business.Consumers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AlbedoTeam.Accounts.Contracts.Common;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using Db;
    using Mappers;
    using MassTransit;

    public class CreateAccountConsumer : IConsumer<CreateAccount>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public CreateAccountConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreateAccount> context)
        {
            var exists =
                (await _repository.FilterBy(t => t.IdentificationNumber.Equals(context.Message.IdentificationNumber)))
                .Any();

            if (exists)
            {
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.AlreadyExists,
                    ErrorMessage = $"Already exists for identification number {context.Message.IdentificationNumber}"
                });
            }
            else
            {
                var account = await _repository.InsertOne(_mapper.MapRequestToModel(context.Message));
                await context.RespondAsync(_mapper.MapModelToResponse(account));
            }
        }
    }
}
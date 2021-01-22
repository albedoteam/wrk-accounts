using System.Linq;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class CreateAccountRequestConsumer : IConsumer<CreateAccountRequest>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public CreateAccountRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<CreateAccountRequest> context)
        {
            var exists = (await _repository.FilterBy(t => t.Name.Equals(context.Message.Name))).Any();
            if (exists)
            {
                await context.RespondAsync<AccountExistsResponse>(new { });
                return;
            }

            var account = await _repository.InsertOne(_mapper.MapRequestToModel(context.Message));
            await context.RespondAsync(_mapper.MapModelToResponse(account));
        }
    }
}
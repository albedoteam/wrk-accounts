using System.Linq;
using System.Threading.Tasks;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class ListAccountsRequestConsumer : IConsumer<ListAccounts>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public ListAccountsRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ListAccounts> context)
        {
            var page = context.Message.Page > 0 ? context.Message.Page : 1;
            var pageSize = context.Message.PageSize <= 1 ? 1 : context.Message.PageSize;

            var (totalPages, accounts) = await _repository.QueryByPage(
                page,
                pageSize,
                a => context.Message.ShowDeleted || !a.IsDeleted,
                a => a.Name);

            await context.RespondAsync<ListAccountsResponse>(new
            {
                context.Message.Page,
                context.Message.PageSize,
                RecordsInPage = accounts.Count,
                TotalPages = totalPages,
                Items = _mapper.MapModelToResponse(accounts.ToList())
            });
        }
    }
}
namespace Accounts.Business.Consumers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AlbedoTeam.Accounts.Contracts.Common;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using AlbedoTeam.Sdk.DataLayerAccess.Utils.Query;
    using Db;
    using Mappers;
    using MassTransit;
    using Models;
    using MongoDB.Driver;

    public class ListAccountsConsumer : IConsumer<ListAccounts>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public ListAccountsConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ListAccounts> context)
        {
            var queryRequest = QueryUtils.GetQueryParams<Account>(_mapper.RequestToQuery(context.Message));
            var queryResponse = await _repository.QueryByPage(queryRequest);

            if (!queryResponse.Records.Any())
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = "Accounts not found"
                });
            else
                await context.RespondAsync<ListAccountsResponse>(new
                {
                    queryResponse.Page,
                    queryResponse.PageSize,
                    queryResponse.RecordsInPage,
                    queryResponse.TotalPages,
                    context.Message.FilterBy,
                    context.Message.OrderBy,
                    context.Message.Sorting,
                    Items = _mapper.MapModelToResponse(queryResponse.Records.ToList())
                });
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using AlbedoTeam.Accounts.Business.Models;
using AlbedoTeam.Accounts.Contracts.Common;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;
using MassTransit;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AlbedoTeam.Accounts.Business.Consumers
{
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
            var page = context.Message.Page > 0 ? context.Message.Page : 1;
            var pageSize = context.Message.PageSize <= 1 ? 1 : context.Message.PageSize;
            var filters = CreateFilters(context);
            var sortBy = CreateSorting(context);

            var (totalPages, accounts) = await _repository.QueryByPage(
                page,
                pageSize,
                filters,
                sortBy);

            await context.RespondAsync<ListAccountsResponse>(new
            {
                context.Message.Page,
                context.Message.PageSize,
                RecordsInPage = accounts.Count,
                TotalPages = totalPages,
                context.Message.FilterBy,
                context.Message.OrderBy,
                context.Message.Sorting,
                Items = _mapper.MapModelToResponse(accounts.ToList())
            });
        }

        private static SortDefinition<Account> CreateSorting(ConsumeContext<ListAccounts> context)
        {
            var orderBy = context.Message.OrderBy;

            var sortBy = context.Message.Sorting == Sorting.Asc
                ? Builders<Account>.Sort.Ascending(new StringFieldDefinition<Account>(orderBy.ToString()))
                : Builders<Account>.Sort.Descending(new StringFieldDefinition<Account>(orderBy.ToString()));

            return sortBy;
        }

        private static FilterDefinition<Account> CreateFilters(ConsumeContext<ListAccounts> context)
        {
            var requiredFilters = Builders<Account>.Filter.And(
                context.Message.ShowDeleted
                    ? Builders<Account>.Filter.Empty
                    : Builders<Account>.Filter.Eq(a => a.IsDeleted, false));

            if (context.Message.FilterBy is null)
                return requiredFilters;

            var optionalFilters = Builders<Account>.Filter.Or(
                requiredFilters
            );

            foreach (var (key, value) in context.Message.FilterBy)
                optionalFilters &= key switch
                {
                    FilterByField.Name => Builders<Account>.Filter.Regex(
                        a => a.Name,
                        new BsonRegularExpression(value, "i")),
                    FilterByField.Description => Builders<Account>.Filter.Regex(
                        a => a.Description,
                        new BsonRegularExpression(value, "i")),
                    FilterByField.IdentificationNumber => Builders<Account>.Filter.Regex(
                        a => a.IdentificationNumber,
                        new BsonRegularExpression(value, "i")),
                    _ => Builders<Account>.Filter.Empty
                };

            return optionalFilters;
        }
    }
}
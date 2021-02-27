using System.Linq;
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

            var filters = _repository.Helpers.CreateFilters(
                context.Message.ShowDeleted,
                null,
                AddFilterBy(context.Message.FilterBy));

            var sortBy = _repository.Helpers.CreateSorting(
                context.Message.OrderBy,
                context.Message.Sorting.ToString());

            var (totalPages, records) = await _repository.QueryByPage(
                page,
                pageSize,
                filters,
                sortBy);

            await context.RespondAsync<ListAccountsResponse>(new
            {
                context.Message.Page,
                context.Message.PageSize,
                RecordsInPage = records.Count,
                TotalPages = totalPages,
                context.Message.FilterBy,
                context.Message.OrderBy,
                context.Message.Sorting,
                Items = _mapper.MapModelToResponse(records.ToList())
            });
        }

        private FilterDefinition<Account> AddFilterBy(string filterBy)
        {
            if (string.IsNullOrWhiteSpace(filterBy))
                return null;

            var optionalFilters = Builders<Account>.Filter.Or(
                _repository.Helpers.Like(a => a.Name, filterBy),
                _repository.Helpers.Like(a => a.DisplayName, filterBy),
                _repository.Helpers.Like(a => a.Description, filterBy),
                _repository.Helpers.Like(a => a.IdentificationNumber, filterBy)
            );

            return optionalFilters;
        }
    }
}
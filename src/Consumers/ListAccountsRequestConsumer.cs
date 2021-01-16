using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using AlbedoTeam.Accounts.Business.Models;
using MassTransit;
using MongoDB.Driver;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class ListAccountsRequestConsumer : IConsumer<ListAccountsRequest>
    {
        private readonly IAccountRepository _repository;
        private readonly IAccountMapper _mapper;

        public ListAccountsRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ListAccountsRequest> context)
        {
            var (totalPages, accounts) = await _repository.QueryByPage(
                context.Message.Page,
                context.Message.PageSize,
                a => context.Message.ShowDeleted || !a.IsDeleted,
                a => a.Name);

            if (!accounts.Any())
                await context.RespondAsync<AccountNotFound>(new { });
            else
                await context.RespondAsync<ListAccountsResponse>(new
                {
                    context.Message.Page,
                    context.Message.PageSize,
                    RecordsInPage = accounts.Count,
                    TotalPages = totalPages,
                    Items = _mapper.MapModelToResponse((List<Account>) accounts)
                });
        }
    }
}
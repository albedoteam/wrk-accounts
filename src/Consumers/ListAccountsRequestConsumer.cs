﻿using System.Linq;
using System.Threading.Tasks;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Business.Db;
using AlbedoTeam.Accounts.Business.Mappers;
using MassTransit;

namespace AlbedoTeam.Accounts.Business.Consumers
{
    public class ListAccountsRequestConsumer : IConsumer<ListAccountsRequest>
    {
        private readonly IAccountMapper _mapper;
        private readonly IAccountRepository _repository;

        public ListAccountsRequestConsumer(IAccountRepository repository, IAccountMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ListAccountsRequest> context)
        {
            var page = context.Message.Page > 0 ? context.Message.Page : 1;
            var pageSize = context.Message.PageSize <= 1 ? 1 : context.Message.PageSize;

            var (totalPages, accounts) = await _repository.QueryByPage(
                page,
                pageSize,
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
                    Items = _mapper.MapModelToResponse(accounts.ToList())
                });
        }
    }
}
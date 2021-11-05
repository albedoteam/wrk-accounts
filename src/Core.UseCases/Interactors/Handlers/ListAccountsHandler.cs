namespace Core.UseCases.Interactors.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Entities;
    using FailFast;
    using InterfaceAdapters;
    using MediatR;
    using Requests;
    using Responses;

    public class ListAccountsHandler : IRequestHandler<ListAccounts, Result<PagedResponse<Account>>>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;

        public ListAccountsHandler(IMapper mapper, IAccountRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<PagedResponse<Account>>> Handle(ListAccounts request,
            CancellationToken cancellationToken)
        {
            var queryResponse = await _repository.QueryByPage(_mapper.Map<ListAccounts, IPagedQueryRequest>(request));
            var response = _mapper.Map<IPagedQueryResponse<Account>, PagedResponse<Account>>(queryResponse);

            return new Result<PagedResponse<Account>>(response);
        }
    }
}
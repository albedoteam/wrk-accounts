namespace Accounts.Entrypoint.gRPC.Services
{
    using System.Threading.Tasks;
    using AccountGrpc;
    using Albedo.Sdk.UseCases.Responses;
    using AutoMapper;
    using Core.Entities;
    using Core.UseCases.Interactors.Requests;
    using Extensions;
    using Grpc.Core;
    using MediatR;

    public class AccountService : Accounts.AccountsBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<AccountResponse> Get(GetRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(_mapper.Map<GetRequest, GetAccount>(request));

            if (response.HasErrors)
                response.ThrowError();

            return _mapper.Map<Account, AccountResponse>(response.Data);
        }

        public override async Task<ListAccountsResponse> List(ListRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(_mapper.Map<ListRequest, ListAccounts>(request));

            if (response.HasErrors)
                response.ThrowError();

            return _mapper.Map<PagedResponse<Account>, ListAccountsResponse>(response.Data);
        }

        public override async Task<AccountResponse> Create(CreateRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(_mapper.Map<CreateRequest, CreateAccount>(request));

            if (response.HasErrors)
                response.ThrowError();

            return _mapper.Map<Account, AccountResponse>(response.Data);
        }

        public override async Task<AccountResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(_mapper.Map<DeleteRequest, DeleteAccount>(request));

            if (response.HasErrors)
                response.ThrowError();

            return _mapper.Map<Account, AccountResponse>(response.Data);
        }

        public override async Task<AccountResponse> Update(UpdateRequest request, ServerCallContext context)
        {
            var response = await _mediator.Send(_mapper.Map<UpdateRequest, UpdateAccount>(request));

            if (response.HasErrors)
                response.ThrowError();

            return _mapper.Map<Account, AccountResponse>(response.Data);
        }
    }
}
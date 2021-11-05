namespace Accounts.Core.UseCases.Interactors.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Albedo.Sdk.UseCases.Enums;
    using Albedo.Sdk.UseCases.FailFast;
    using Entities;
    using InterfaceAdapters;
    using MediatR;
    using Requests;

    public class GetAccountHandler : IRequestHandler<GetAccount, Result<Account>>
    {
        private readonly IAccountRepository _repository;

        public GetAccountHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(GetAccount request, CancellationToken cancellationToken)
        {
            var account = await _repository.FindById(request.Id, request.ShowDeleted);

            return account is { }
                ? new Result<Account>(account)
                : new Result<Account>(ErrorType.NotFound);
        }
    }
}
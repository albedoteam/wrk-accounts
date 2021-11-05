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

    public class DeleteAccountHandler : IRequestHandler<DeleteAccount, Result<Account>>
    {
        private readonly IAccountRepository _repository;

        public DeleteAccountHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(DeleteAccount request, CancellationToken cancellationToken)
        {
            var account = await _repository.FindById(request.Id);
            if (account is null)
                return new Result<Account>(ErrorType.NotFound);

            await _repository.DeleteById(request.Id);

            // get "soft-deleted" account
            account = await _repository.FindById(request.Id, true);

            return new Result<Account>(account);
        }
    }
}
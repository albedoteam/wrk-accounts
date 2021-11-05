namespace Accounts.Core.UseCases.Interactors.Requests
{
    using Albedo.Sdk.UseCases.FailFast;
    using Entities;
    using MediatR;

    public class GetAccount : IRequest<Result<Account>>
    {
        public string Id { get; set; }
        public bool ShowDeleted { get; set; }
    }
}
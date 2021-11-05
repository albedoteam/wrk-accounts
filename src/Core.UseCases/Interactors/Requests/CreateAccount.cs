namespace Accounts.Core.UseCases.Interactors.Requests
{
    using Albedo.Sdk.UseCases.FailFast;
    using Entities;
    using MediatR;

    public class CreateAccount : IRequest<Result<Account>>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Enabled { get; set; }
    }
}
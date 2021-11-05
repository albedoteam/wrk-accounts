namespace Core.UseCases.Interactors.Requests
{
    using Albedo.Sdk.UseCases.FailFast;
    using Entities;
    using MediatR;

    public class DeleteAccount : IRequest<Result<Account>>
    {
        public string Id { get; set; }
    }
}
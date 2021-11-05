namespace Core.UseCases.Interactors.Requests
{
    using Entities;
    using FailFast;
    using MediatR;

    public class DeleteAccount : IRequest<Result<Account>>
    {
        public string Id { get; set; }
    }
}
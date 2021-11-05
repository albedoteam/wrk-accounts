namespace Core.UseCases.Interactors.Requests
{
    using Entities;
    using FailFast;
    using MediatR;

    public class GetAccount : IRequest<Result<Account>>
    {
        public string Id { get; set; }
        public bool ShowDeleted { get; set; }
    }
}
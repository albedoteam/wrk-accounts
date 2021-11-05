namespace Core.UseCases.Interactors.Requests
{
    using Entities;
    using Enums;
    using FailFast;
    using MediatR;
    using Responses;

    public class ListAccounts : IRequest<Result<PagedResponse<Account>>>
    {
        public bool ShowDeleted { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string FilterBy { get; set; }
        public string OrderBy { get; set; }
        public Sorting Sorting { get; set; }
    }
}
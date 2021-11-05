namespace Accounts.Core.UseCases.Interactors.Requests
{
    using Albedo.Sdk.UseCases.Enums;
    using Albedo.Sdk.UseCases.FailFast;
    using Albedo.Sdk.UseCases.Responses;
    using Entities;
    using MediatR;

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
namespace Albedo.Sdk.UseCases.Abstractions
{
    using Enums;

    public interface IPagedRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
        string FilterBy { get; set; }
        string OrderBy { get; set; }
        Sorting Sorting { get; set; }
    }
}
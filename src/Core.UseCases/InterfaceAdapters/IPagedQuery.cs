namespace Core.UseCases.InterfaceAdapters
{
    using Enums;

    public interface IPagedQuery
    {
        int Page { get; set; }
        int PageSize { get; set; }
        string FilterBy { get; set; }
        string OrderBy { get; set; }
        Sorting Sorting { get; set; }
    }
}
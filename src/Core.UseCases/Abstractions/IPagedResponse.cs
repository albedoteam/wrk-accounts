namespace Core.UseCases.Abstractions
{
    using System.Collections.Generic;
    using Enums;

    public interface IPagedResponse<T>
    {
        int Page { get; set; }
        int PageSize { get; set; }
        int RecordsInPage { get; set; }
        int TotalPages { get; set; }
        string FilterBy { get; set; }
        string OrderBy { get; set; }
        Sorting Sorting { get; set; }
        List<T> Items { get; set; }
    }
}
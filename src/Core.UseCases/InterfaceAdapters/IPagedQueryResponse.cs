namespace Core.UseCases.InterfaceAdapters
{
    using System.Collections.Generic;

    public interface IPagedQueryResponse<T> : IPagedQuery
    {
        int RecordsInPage { get; set; }
        int TotalPages { get; set; }
        List<T> Items { get; set; }
    }
}
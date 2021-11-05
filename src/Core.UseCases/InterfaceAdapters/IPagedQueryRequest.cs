namespace Core.UseCases.InterfaceAdapters
{
    public interface IPagedQueryRequest : IPagedQuery
    {
        bool ShowDeleted { get; set; }
    }
}
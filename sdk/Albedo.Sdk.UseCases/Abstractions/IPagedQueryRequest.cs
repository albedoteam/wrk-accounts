namespace Albedo.Sdk.UseCases.Abstractions
{
    public interface IPagedQueryRequest : IPagedQuery
    {
        bool ShowDeleted { get; set; }
    }
}
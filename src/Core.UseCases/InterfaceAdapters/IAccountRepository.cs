namespace Accounts.Core.UseCases.InterfaceAdapters
{
    using System.Threading.Tasks;
    using Albedo.Sdk.UseCases.Abstractions;
    using Entities;

    public interface IAccountRepository
    {
        Task<bool> Exists(string identificationNumber);
        Task<Account> InsertOne(Account account);
        Task<Account> FindById(string id, bool showDeleted = false);
        Task DeleteById(string id);
        Task UpdateById(string id, Account account);
        Task<IPagedQueryResponse<Account>> QueryByPage(IPagedQueryRequest pagedRequest);
    }
}
using Accounts.Business.Models;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;

namespace Accounts.Business.Db
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
    }
}
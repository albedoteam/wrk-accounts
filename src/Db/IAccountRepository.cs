using AlbedoTeam.Accounts.Business.Models;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;

namespace AlbedoTeam.Accounts.Business.Db
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
    }
}
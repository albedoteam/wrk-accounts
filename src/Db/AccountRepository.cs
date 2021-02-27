using AlbedoTeam.Accounts.Business.Models;
using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;

namespace AlbedoTeam.Accounts.Business.Db
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IDbContext<Account> context, IHelpers<Account> helpers) : base(context, helpers)
        {
        }
    }
}
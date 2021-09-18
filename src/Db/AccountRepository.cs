namespace Accounts.Business.Db
{
    using AlbedoTeam.Sdk.DataLayerAccess;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using Models;

    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IDbContext<Account> context, IHelpers<Account> helpers) : base(context, helpers)
        {
        }
    }
}
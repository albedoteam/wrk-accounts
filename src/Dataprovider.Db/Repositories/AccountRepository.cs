namespace Dataprovider.Db.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Albedo.Sdk.UseCases.Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess;
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess.Utils.Query;
    using AutoMapper;
    using Core.Entities;
    using Core.UseCases.InterfaceAdapters;
    using Models;
    using MongoDB.Driver;

    public class AccountRepository : BaseRepository<AccountModel>, IAccountRepository
    {
        private readonly IMapper _mapper;

        public AccountRepository(
            IDbContext<AccountModel> context,
            IHelpers<AccountModel> helpers,
            IMapper mapper)
            : base(context, helpers)
        {
            _mapper = mapper;
        }

        public async Task<bool> Exists(string identificationNumber)
        {
            return (await FilterBy(a => a.IdentificationNumber.Equals(identificationNumber))).Any();
        }

        public async Task<Account> InsertOne(Account account)
        {
            var model = _mapper.Map<Account, AccountModel>(account);

            model = await base.InsertOne(model);

            return _mapper.Map<AccountModel, Account>(model);
        }

        public async Task<Account> FindById(string id, bool showDeleted = false)
        {
            var model = await base.FindById(id, showDeleted);

            return _mapper.Map<AccountModel, Account>(model);
        }

        public async Task DeleteById(string id)
        {
            await base.DeleteById(id);
        }

        public async Task UpdateById(string id, Account account)
        {
            var update = Builders<AccountModel>.Update.Combine(
                Builders<AccountModel>.Update.Set(a => a.DisplayName, account.DisplayName),
                Builders<AccountModel>.Update.Set(a => a.Description, account.Description),
                Builders<AccountModel>.Update.Set(a => a.IdentificationNumber, account.IdentificationNumber),
                Builders<AccountModel>.Update.Set(a => a.Enabled, account.Enabled));

            await base.UpdateById(id, update);
        }

        public async Task<IPagedQueryResponse<Account>> QueryByPage(IPagedQueryRequest queryRequest)
        {
            var queryParams = _mapper.Map<IPagedQueryRequest, QueryParams>(queryRequest);

            var request = QueryUtils.GetQueryParams<AccountModel>(queryParams);
            var response = await base.QueryByPage(request);

            return _mapper.Map<QueryResponse<AccountModel>, IPagedQueryResponse<Account>>(response);
        }
    }
}
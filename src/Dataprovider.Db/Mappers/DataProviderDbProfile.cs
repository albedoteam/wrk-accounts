namespace Accounts.Dataprovider.Db.Mappers
{
    using Albedo.Sdk.UseCases.Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess.Utils.Query;
    using AutoMapper;
    using Core.Entities;
    using Models;
    using MongoDB.Bson;

    public class DataProviderDbProfile : Profile
    {
        public DataProviderDbProfile()
        {
            CreateMap<Account, AccountModel>(MemberList.Destination)
                .ForMember(m => m.Id, opt => opt.MapFrom(e => new ObjectId(e.Id)));

            CreateMap<AccountModel, Account>(MemberList.Destination)
                .ForMember(e => e.Id, opt => opt.MapFrom(m => m.ToString()));

            CreateMap<IPagedQueryRequest, QueryParams>(MemberList.Destination);

            CreateMap<QueryResponse<AccountModel>, IPagedQueryResponse<Account>>(MemberList.Destination)
                .ForMember(r => r.Items, opt => opt.MapFrom(l => l.Records));
        }
    }
}
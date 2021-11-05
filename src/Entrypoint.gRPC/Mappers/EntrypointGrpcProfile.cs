namespace Accounts.Business.Mappers
{
    using AccountGrpc;
    using AutoMapper;
    using Core.Entities;
    using Core.UseCases.Interactors.Requests;
    using Core.UseCases.Interactors.Responses;

    public class EntrypointGrpcProfile : Profile
    {
        public EntrypointGrpcProfile()
        {
            // requests
            CreateMap<GetRequest, GetAccount>().ReverseMap();
            CreateMap<ListRequest, ListAccounts>().ReverseMap();
            CreateMap<CreateRequest, CreateAccount>().ReverseMap();
            CreateMap<UpdateRequest, UpdateAccount>().ReverseMap();
            CreateMap<DeleteRequest, DeleteAccount>().ReverseMap();

            // responses
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<PagedResponse<Account>, ListAccountsResponse>(MemberList.Destination);
        }
    }
}
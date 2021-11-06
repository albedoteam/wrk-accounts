namespace Accounts.Entrypoint.gRPC.Mappers
{
    using AccountGrpc;
    using Albedo.Sdk.UseCases.Responses;
    using AutoMapper;
    using Core.Entities;
    using Core.UseCases.Interactors.Requests;
    using Google.Protobuf.WellKnownTypes;

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
            CreateMap<Account, AccountResponse>(MemberList.Destination)
                .ForMember(ar => ar.CreatedAt, opt => opt.MapFrom(a => a.CreatedAt.ToTimestamp()));

            CreateMap<PagedResponse<Account>, ListAccountsResponse>(MemberList.Destination)
                .ForMember(lar => lar.FilterBy, opt => opt.MapFrom(pr => pr.FilterBy ?? ""))
                .ForMember(lar => lar.OrderBy, opt => opt.MapFrom(pr => pr.OrderBy ?? ""));
        }
    }
}
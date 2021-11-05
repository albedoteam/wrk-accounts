namespace Core.UseCases.Mappers
{
    using Albedo.Sdk.UseCases.Abstractions;
    using Albedo.Sdk.UseCases.Responses;
    using AutoMapper;
    using Entities;
    using Interactors.Requests;

    public class CoreUseCasesProfile : Profile
    {
        public CoreUseCasesProfile()
        {
            CreateMap<CreateAccount, Account>(MemberList.Destination);
            CreateMap<UpdateAccount, Account>(MemberList.Destination);
            CreateMap<ListAccounts, IPagedQueryRequest>(MemberList.Destination);
            CreateMap<IPagedQueryResponse<Account>, PagedResponse<Account>>(MemberList.Destination);
        }
    }
}
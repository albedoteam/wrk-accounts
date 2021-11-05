namespace Core.UseCases.Mappers
{
    using AutoMapper;
    using Entities;
    using Interactors.Requests;
    using Interactors.Responses;
    using InterfaceAdapters;

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
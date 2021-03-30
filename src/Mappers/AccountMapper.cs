namespace Accounts.Business.Mappers
{
    using System.Collections.Generic;
    using AlbedoTeam.Accounts.Contracts.Events;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using AutoMapper;
    using Models;

    public class AccountMapper : IAccountMapper
    {
        private readonly IMapper _mapper;

        public AccountMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // request to model
                cfg.CreateMap<Account, CreateAccount>().ReverseMap();

                // model to response
                cfg.CreateMap<Account, AccountResponse>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));

                // model to event
                cfg.CreateMap<Account, AccountDeleted>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));

                cfg.CreateMap<Account, AccountUpdated>(MemberList.Destination)
                    .ForMember(t => t.Id, opt => opt.MapFrom(o => o.Id.ToString()));
            });

            _mapper = config.CreateMapper();
        }

        public Account MapRequestToModel(CreateAccount request)
        {
            return _mapper.Map<CreateAccount, Account>(request);
        }

        public List<AccountResponse> MapModelToResponse(List<Account> modelList)
        {
            return _mapper.Map<List<Account>, List<AccountResponse>>(modelList);
        }

        public AccountResponse MapModelToResponse(Account model)
        {
            return _mapper.Map<Account, AccountResponse>(model);
        }

        public AccountDeleted MapModelToDeletedEvent(Account model)
        {
            return _mapper.Map<Account, AccountDeleted>(model);
        }

        public AccountUpdated MapModelToUpdatedEvent(Account model)
        {
            return _mapper.Map<Account, AccountUpdated>(model);
        }
    }
}
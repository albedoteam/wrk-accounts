using System.Collections.Generic;
using Accounts.Contracts.Events;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Business.Models;
using AutoMapper;

namespace AlbedoTeam.Accounts.Business.Mappers
{
    public class AccountMapper : IAccountMapper
    {
        private readonly IMapper _mapper;

        public AccountMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // request to model
                cfg.CreateMap<Account, CreateAccountRequest>().ReverseMap();

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

        public Account MapRequestToModel(CreateAccountRequest request)
        {
            return _mapper.Map<CreateAccountRequest, Account>(request);
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
using System.Collections.Generic;
using Accounts.Business.Models;
using AlbedoTeam.Accounts.Contracts.Events;
using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Accounts.Contracts.Responses;

namespace Accounts.Business.Mappers
{
    public interface IAccountMapper
    {
        // request to model
        Account MapRequestToModel(CreateAccount request);
        List<AccountResponse> MapModelToResponse(List<Account> modelList);

        // model to response
        AccountResponse MapModelToResponse(Account model);

        // model to event
        AccountDeleted MapModelToDeletedEvent(Account model);
        AccountUpdated MapModelToUpdatedEvent(Account model);
    }
}
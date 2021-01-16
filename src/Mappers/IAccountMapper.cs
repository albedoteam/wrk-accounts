using System.Collections.Generic;
using Accounts.Contracts.Events;
using Accounts.Contracts.Requests;
using Accounts.Contracts.Responses;
using AlbedoTeam.Accounts.Business.Models;

namespace AlbedoTeam.Accounts.Business.Mappers
{
    public interface IAccountMapper
    {
        // request to model
        Account MapRequestToModel(CreateAccountRequest request);
        List<AccountResponse> MapModelToResponse(List<Account> modelList);

        // model to response
        AccountResponse MapModelToResponse(Account model);

        // model to event
        AccountDeleted MapModelToDeletedEvent(Account model);
        AccountUpdated MapModelToUpdatedEvent(Account model);
    }
}
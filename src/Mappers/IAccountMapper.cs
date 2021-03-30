namespace Accounts.Business.Mappers
{
    using System.Collections.Generic;
    using AlbedoTeam.Accounts.Contracts.Events;
    using AlbedoTeam.Accounts.Contracts.Requests;
    using AlbedoTeam.Accounts.Contracts.Responses;
    using Models;

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
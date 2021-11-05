namespace Core.UseCases.Interactors.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Albedo.Sdk.UseCases.Enums;
    using Albedo.Sdk.UseCases.FailFast;
    using AutoMapper;
    using Entities;
    using InterfaceAdapters;
    using MediatR;
    using Requests;

    public class UpdateAccountHandler : IRequestHandler<UpdateAccount, Result<Account>>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;

        public UpdateAccountHandler(IMapper mapper, IAccountRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(UpdateAccount request, CancellationToken cancellationToken)
        {
            var account = await _repository.FindById(request.Id);
            if (account is null)
                return new Result<Account>(ErrorType.NotFound);

            account = _mapper.Map<UpdateAccount, Account>(request);

            await _repository.UpdateById(request.Id, account);

            // get "updated" account
            account = await _repository.FindById(request.Id);

            return new Result<Account>(account);
        }
    }
}
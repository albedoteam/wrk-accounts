namespace Core.UseCases.Interactors.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Entities;
    using Enums;
    using FailFast;
    using InterfaceAdapters;
    using MediatR;
    using Requests;

    public class CreateAccountHandler : IRequestHandler<CreateAccount, Result<Account>>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;

        public CreateAccountHandler(IMapper mapper, IAccountRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(CreateAccount request, CancellationToken cancellationToken)
        {
            var exists = await _repository.Exists(request.IdentificationNumber);

            if (exists)
                return new Result<Account>(ErrorType.AlreadyExists);

            var account = await _repository.InsertOne(_mapper.Map<CreateAccount, Account>(request));

            return new Result<Account>(account);
        }
    }
}
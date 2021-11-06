namespace Accounts.Core.UseCases.Interactors.Validations
{
    using FluentValidation;
    using Requests;

    public class CreateAccountValidator : AbstractValidator<CreateAccount>
    {
        public CreateAccountValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty();

            RuleFor(c => c.DisplayName)
                .NotEmpty();

            RuleFor(c => c.IdentificationNumber)
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotEmpty();
        }
    }
}
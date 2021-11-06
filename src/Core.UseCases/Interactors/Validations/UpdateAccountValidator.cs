namespace Accounts.Core.UseCases.Interactors.Validations
{
    using System.Text.RegularExpressions;
    using FluentValidation;
    using Requests;

    public class UpdateAccountValidator: AbstractValidator<UpdateAccount>
    {
        public UpdateAccountValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .Matches("^[0-9a-fA-F]{24}$", RegexOptions.IgnoreCase);

            RuleFor(c => c.DisplayName)
                .NotEmpty();

            RuleFor(c => c.IdentificationNumber)
                .NotEmpty();

            RuleFor(c => c.Description)
                .NotEmpty();
        }
    }
}
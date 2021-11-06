namespace Accounts.Core.UseCases.Interactors.Validations
{
    using System.Text.RegularExpressions;
    using FluentValidation;
    using Requests;

    public class GetAccountValidator: AbstractValidator<GetAccount>
    {
        public GetAccountValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .Matches("^[0-9a-fA-F]{24}$", RegexOptions.IgnoreCase);
        }
    }
}
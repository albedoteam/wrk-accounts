namespace Core.UseCases.Interactors.Responses
{
    using Enums;

    public class ErrorResponse
    {
        public ErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
namespace Albedo.Sdk.UseCases.Responses
{
    using Enums;

    public class ErrorResponse
    {
        public ErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
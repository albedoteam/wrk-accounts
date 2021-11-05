namespace Albedo.Sdk.UseCases.Abstractions
{
    using Enums;

    public interface IResult
    {
        IResult AddError(string errorCode, string errorMessage);
        void SetErrorType(ErrorType errorType);
    }
}
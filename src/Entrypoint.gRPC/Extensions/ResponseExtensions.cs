namespace Accounts.Business.Extensions
{
    using Core.UseCases.Enums;
    using Core.UseCases.FailFast;
    using Grpc.Core;

    public static class ResponseExtensions
    {
        public static void ThrowError<T>(this Result<T> response)
        {
            if (response is null)
                throw new RpcException(new Status(StatusCode.Internal, "Response is null"));

            throw response.ErrorType switch
            {
                ErrorType.AlreadyExists => RpcException(StatusCode.AlreadyExists),
                ErrorType.BadRequest => RpcException(StatusCode.InvalidArgument),
                ErrorType.InvalidOperation => RpcException(StatusCode.FailedPrecondition),
                ErrorType.NotFound => RpcException(StatusCode.NotFound),
                ErrorType.InternalServerError => RpcException(StatusCode.Internal),
                _ => RpcException(StatusCode.Internal)
            };

            RpcException RpcException(StatusCode statusCode)
            {
                var metadata = new Metadata();
                foreach (var responseError in response.Errors)
                    metadata.Add(new Metadata.Entry(responseError.Key, responseError.Value));

                return new RpcException(new Status(statusCode, statusCode.ToString()), metadata);
            }
        }
    }
}
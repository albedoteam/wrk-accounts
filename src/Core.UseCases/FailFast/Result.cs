namespace Core.UseCases.FailFast
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Abstractions;
    using Enums;

    public class Result<TData> : IResult
    {
        private readonly IList<ErrorEntry> _errors = new List<ErrorEntry>();

        public Result()
        {
            ErrorType = ErrorType.InternalServerError;
            Errors = new ReadOnlyCollection<ErrorEntry>(_errors);
        }

        public Result(ErrorType errorType, string errorMessage = null)
        {
            ErrorType = errorType;
            _errors.Add(new ErrorEntry(errorType.ToString(), errorMessage));
            Errors = new ReadOnlyCollection<ErrorEntry>(_errors);
        }

        public Result(TData data)
        {
            Data = data;
            Errors = new ReadOnlyCollection<ErrorEntry>(_errors);
        }

        public IEnumerable<ErrorEntry> Errors { get; set; }
        public TData Data { get; set; }
        public bool HasErrors => Errors.Any();
        public ErrorType ErrorType { get; private set; }

        public IResult AddError(string errorCode, string errorMessage)
        {
            _errors.Add(new ErrorEntry(errorCode, errorMessage));
            return this;
        }

        public void SetErrorType(ErrorType errorType)
        {
            ErrorType = errorType;
        }
    }
}
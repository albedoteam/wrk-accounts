namespace Core.UseCases.FailFast
{
    public class ErrorEntry
    {
        public ErrorEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }
}
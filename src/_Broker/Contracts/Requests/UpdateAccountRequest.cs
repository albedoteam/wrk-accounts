namespace Accounts.Contracts.Requests
{
    public interface UpdateAccountRequest
    {
        string Id { get; set; }
        string Name { get; set; }
        bool Enabled { get; set; }
    }
}
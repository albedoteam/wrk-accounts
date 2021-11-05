namespace Accounts.Dataprovider.Db.Models
{
    using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
    using AlbedoTeam.Sdk.DataLayerAccess.Attributes;

    [Collection("Accounts")]
    public class AccountModel : Document
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Enabled { get; set; }
    }
}
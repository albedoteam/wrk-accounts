using AlbedoTeam.Sdk.DataLayerAccess.Abstractions;
using AlbedoTeam.Sdk.DataLayerAccess.Attributes;

namespace AlbedoTeam.Accounts.Business.Models
{
    [BsonCollection("Accounts")]
    public class Account : Document
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
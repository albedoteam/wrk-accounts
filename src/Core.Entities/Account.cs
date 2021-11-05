namespace Core.Entities
{
    using Abstractions;

    public class Account : BaseEntity<string>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Enabled { get; set; }
    }
}
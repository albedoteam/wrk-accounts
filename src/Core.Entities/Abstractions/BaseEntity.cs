namespace Core.Entities.Abstractions
{
    using System;

    public abstract class BaseEntity<TIdType> : IEntity<TIdType>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public TIdType Id { get; set; }
    }
}
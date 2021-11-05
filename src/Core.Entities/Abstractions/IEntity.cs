﻿namespace Core.Entities.Abstractions
{
    public interface IEntity<TIdType>
    {
        TIdType Id { get; set; }
    }
}
﻿namespace MongoDbRepository.Base
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
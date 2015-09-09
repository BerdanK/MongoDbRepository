using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbRepository.Base
{
    public abstract class Entity : IEntity<ObjectId>
    {
        [BsonId]
        public virtual ObjectId Id { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbRepository.Base
{
    public abstract class Entity : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }
    }
}

using MongoDbRepository.Base;

namespace MongoDbRepository.Model
{
    [CollectionName("User")]
    public class User : Entity
    {
        public string Adi { get; set; }

        public string Soyadi { get; set; }
    }
}

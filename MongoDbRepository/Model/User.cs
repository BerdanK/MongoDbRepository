using MongoDbRepository.Base;

namespace MongoDbRepository.Model
{
    public class User : Entity
    {
        public string Adi { get; set; }

        public string Soyadi { get; set; }
    }
}

using MongoDB.Bson;
using MongoDbRepository.Model;
using System;
using System.Threading.Tasks;

namespace MongoDbRepository
{
    class Program
    {
        static void Main(string[] args)
        {
            DoIt().Wait();
        }

        private static async Task DoIt()
        {
            try
            {
                var userRepository = new UserRepository();
                var user = await userRepository.FindBy(x => x.Id == new ObjectId("55f018a802872f23a4901802") && x.Soyadi == "Soy");

                //await userRepository.Add(new User { Id = ObjectId.GenerateNewId(), Adi = "Deneme", Soyadi = "Soyyy" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using MongoDB.Bson;
using MongoDB.Driver;
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
                //var user = await userRepository.FindBy(x => x.Id == new ObjectId("55f018a802872f23a4901802") && x.Soyadi == "Soy");

                //ProjectionDefinition<User, UserView> deneme = new ProjectD
                var users =
                    await
                        userRepository.GetAll<UserView>(x => true,
                            Builders<User>.Projection.Expression(u => new UserView { Adi = u.Adi, Id = u.Id }));
                //await userRepository.Add(new User { Id = ObjectId.GenerateNewId(), Adi = "Deneme", Soyadi = "Soyyy" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

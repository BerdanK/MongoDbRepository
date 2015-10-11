using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Configuration;

namespace MongoDbRepository
{
    public static class Utils
    {
        private const string ConnectionStringName = "MongoBerdanKocaWebConnection";

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
        }

        public static string GetDatabaseName()
        {
            return ConfigurationManager.AppSettings["Database"];
        }

        public static string GetCollectionName<T>()
        {
            var attribute = Attribute.GetCustomAttribute(typeof (T), typeof (CollectionName));
            return attribute == null ? typeof (T).Name : ((CollectionName) attribute).Name;
        }

        public static MongoClientSettings GetMongoClientSettings()
        {
            return new MongoClientSettings
            {
                Servers = new List<MongoServerAddress> {new MongoServerAddress(GetServer(), GetPort())},
                Credentials =
                    new List<MongoCredential>
                    {
                        MongoCredential.CreateCredential(GetDatabaseName(), GetUser(), GetPassword())
                    }
            };
        }

        private static string GetServer()
        {
            return ConfigurationManager.AppSettings["Server"];
        }
        private static int GetPort()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        }
        private static string GetUser()
        {
            return ConfigurationManager.AppSettings["UserName"];
        }
        private static string GetPassword()
        {
            return ConfigurationManager.AppSettings["Password"];
        }
    }
}

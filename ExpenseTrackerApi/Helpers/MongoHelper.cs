using MongoDB.Driver;
using System.Configuration;

namespace ExpenseTrackerWeb.Helpers
{
    public class MongoHelper<T>
    {
        public IMongoCollection<T> Collection { get; private set; }

        public MongoHelper()
        {
            string uriMongo = ConfigurationManager.AppSettings.Get("MONGOLAB_URI");

            var client = new MongoClient(uriMongo);

            string databaseName = MongoUrl.Create(uriMongo).DatabaseName;
            
            var database = client.GetDatabase(databaseName);  

            Collection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}
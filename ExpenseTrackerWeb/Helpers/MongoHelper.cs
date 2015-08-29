using System;
using System.Collections.Generic;
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

            var database = client.GetDatabase("ExpenseTracker");

            Collection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}
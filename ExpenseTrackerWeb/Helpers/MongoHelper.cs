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
            var client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDBConnectionString"].ConnectionString);

            var database = client.GetDatabase("ExpenseTracker");

            Collection = database.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}
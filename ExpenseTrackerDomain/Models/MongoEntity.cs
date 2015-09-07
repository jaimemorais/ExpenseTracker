using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTrackerDomain.Models
{
    public class MongoEntity
    {

        [BsonId]
        public ObjectId Id { get; set; }

    }
}
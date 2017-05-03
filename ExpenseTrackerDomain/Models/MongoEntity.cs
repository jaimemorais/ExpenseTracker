using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExpenseTrackerDomain.Models
{
    public class MongoEntity
    {

        [BsonId]
        public ObjectId _id { get; set; }

        public string Id
        {
            get { return _id.ToString(); }
            set { _id = ObjectId.Parse(value); }
        }
    }
}
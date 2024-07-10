using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using TT.Mongo.Client;

namespace MongoCrud.API.Models.Domain
{
    public class Student :BaseMongoEntity
    {
       // public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}

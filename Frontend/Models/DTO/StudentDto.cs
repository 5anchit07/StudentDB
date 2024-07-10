using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Frontend.Models.DTO
{
    public class StudentDto
    {
        /*  public int Id { get; set; }*/
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}

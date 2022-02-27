using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Maya.FormsConstructionKit.Api.Model.Storage
{
    public class Dingo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int Position { get; set; }

        public string? Name { get; set; }

        public bool IsRingo { get; set; }

        public IList<Dingo>? Dingos { get; set; }
    }
}

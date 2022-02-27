using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Maya.FormsConstructionKit.Api.Model.Storage
{
    public class EntityCsvDefinition
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        /// <summary>
        /// Date of the entity modification or creation
        /// </summary>
        public DateTime VersionDate { get; set; }

        /// <summary>
        /// An unique entity name
        /// </summary>
        public string EntityName { get; set; } = null!;
        
        public string Name { get; set; } = null!;

        public bool SanitizeForInjection { get; set; }

        public string Delimiter { get; set; } = null!;

        public List<CsvColumn> Columns { get; set; } = null!;
    }
}

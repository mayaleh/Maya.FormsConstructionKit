using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Maya.FormsConstructionKit.Api.Model.Storage
{
    public class EntityDefinition
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
        public string Name { get; set; } = null!;

        /// <summary>
        /// The property name, that is used as identification for updating / deleting
        /// the name must exists in the property definition
        /// the client app should not allow to insert name, that does not existing in the properties definition
        /// in the data, the value must be unique
        /// </summary>
        public string KeyPropertyName { get; set; } = null!;

        /// <summary>
        /// Internal identifier. Same for all entity versions
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Properties
        /// </summary>
        public PropertyDefinition[] Properties { get; set; } = null!;

        // TODO webhooks, datasource (this app  DB or another API)

        /// <summary>
        /// definition, where the data is stored
        /// </summary>
        public DataSourceDefinition DataSourceDefinition { get; set; } = null!;
    }
}

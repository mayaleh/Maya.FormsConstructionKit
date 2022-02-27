using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Maya.FormsConstructionKit.Api.Model.Storage
{
    public class EntityValue
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

        public List<PropertyValue> Value { get; set; } = null!;
    }
}

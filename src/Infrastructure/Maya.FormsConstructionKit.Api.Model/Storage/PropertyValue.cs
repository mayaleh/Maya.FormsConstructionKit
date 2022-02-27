using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Maya.FormsConstructionKit.Api.Model.Storage
{
    public class PropertyValue
    {
        public string Name { get; set; } = null!;

        public ContentType Type { get; set; }
        
        [BsonIgnoreIfNull]
        public string? TextVal { get; set; }
        
        [BsonIgnoreIfNull]
        public decimal? DecimalVal { get; set; }
        
        [BsonIgnoreIfNull]
        public float? FloatVal { get; set; }

        [BsonIgnoreIfNull]
        public int? IntVal { get; set; }

        [BsonIgnoreIfNull]
        public long? LongVal { get; set; }

        [BsonIgnoreIfNull]
        public bool? BoolVal { get; set; }

        [BsonIgnoreIfNull]
        public DateOnly? DateVal { get; set; }

        [BsonIgnoreIfNull]
        public DateTime? DateTimeVal { get; set; }

        [BsonIgnoreIfNull] 
        public string? File { get; set; }

        [BsonIgnoreIfNull]
        public string? FileName { get; set; }

        [BsonIgnoreIfNull]
        public string? FileContentType { get; set; }
    }
}

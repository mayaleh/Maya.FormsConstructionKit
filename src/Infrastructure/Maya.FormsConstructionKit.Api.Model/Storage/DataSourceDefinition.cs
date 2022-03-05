using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Maya.FormsConstructionKit.Api.Model.Storage
{
    public class DataSourceDefinition
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        public string AuthToken { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int TimeoutSeconds { get; set; }

        public DataSourceAuthKind AuthKind { get; set; }

        public DataSourceType Type { get; set; }

        public string Endpoint { get; set; } = string.Empty;

        public string ReadPath { get; set; } = string.Empty;
        
        public DataSourceHttpMethodKind ReadHttpMethod { get; set; }

        public string CreatePath { get; set; } = string.Empty;
        
        public DataSourceHttpMethodKind CreateHttpMethod { get; set; }

        public string UpdatePath { get; set; } = string.Empty;
        
        public DataSourceHttpMethodKind UpdateHttpMethod { get; set; }

        public string DeletePath { get; set; } = string.Empty;

        public DataSourceHttpMethodKind DeleteHttpMethod { get; set; }

        public Dictionary<string, string> QueryParams { get; set; } = null!;
    }

    public enum DataSourceAuthKind
    {
        None,
        Basic,
        Bearer,
        OAuthService2Service
    }

    public enum DataSourceHttpMethodKind
    {
        Get,
        Post,
        Put,
        Delete
    }

    public enum DataSourceType
    {
        Storage,
        Api
    }
}

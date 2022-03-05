using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Shared.Model
{
    public class DataSource
    {
        public DataSource()
        {
            QueryParams = new();
        }

        public string Description { get; set; } = string.Empty;

        public string AuthToken { get; set; } = string.Empty;
        
        public string UserName { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty;

        public DataSourceType Type { get; set; }

        public DataSourceAuthKind AuthKind { get; set; }

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

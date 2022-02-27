using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Api.Model.Settings
{
    public class AppSettings : IAppSettings
    {
        public OAuthentication OAuthentication { get; set; } = null!;
        public MongoDataSource MongoDataSource { get; set; } = null!;

        public string[] AllowedOrigins { get; set; } = null!;
    }
}

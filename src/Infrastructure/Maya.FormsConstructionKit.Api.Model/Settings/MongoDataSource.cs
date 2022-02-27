using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Api.Model.Settings
{
    public class MongoDataSource
    {
        public string ConnectionString { get; set; } = null!;

        public string Database { get; set; } = null!;

    }
}

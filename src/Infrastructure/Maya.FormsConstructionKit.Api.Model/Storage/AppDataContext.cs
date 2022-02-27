using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Api.Model.Settings;
using MongoDB.Driver;

namespace Maya.FormsConstructionKit.Api.Model.Storage
{
    public class AppDataContext
    {
        private readonly IMongoDatabase database;

        public AppDataContext(IAppSettings settings)
        {
            var client = new MongoClient(settings.MongoDataSource.ConnectionString);

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            this.database = client.GetDatabase(settings.MongoDataSource.Database);
        }

        public IMongoCollection<Dingo> Dingos
        {
            get
            {
                return this.database.GetCollection<Dingo>(nameof(Dingo));
            }
        }

        public IMongoCollection<EntityDefinition> EntityDefinitions
        {
            get
            {
                return this.database.GetCollection<EntityDefinition>(nameof(EntityDefinition));
            }
        }

        public IMongoCollection<EntityValue> EntityValues
        {
            get
            {
                return this.database.GetCollection<EntityValue>(nameof(EntityValue));
            }
        }
        public IMongoCollection<EntityCsvDefinition> EntityCsvDefinitions
        {
            get
            {
                return this.database.GetCollection<EntityCsvDefinition>(nameof(EntityCsvDefinition));
            }
        }
    }
}

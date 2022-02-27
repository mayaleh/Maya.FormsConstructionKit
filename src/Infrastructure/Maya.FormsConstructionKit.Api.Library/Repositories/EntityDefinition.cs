using MongoDB.Bson;
using MongoDB.Driver;

namespace Maya.FormsConstructionKit.Api.Library.Repositories
{
    public static class EntityDefinition
    {
        public static Task<entityDefinitionsResult> AllAsync(AppDataContext dataContext)
        {
            try
            {
                // Not supported
                //var data = await dataContext.EntityDefinitions.Aggregate()
                //.Group(y => y.Name,
                //       z => z.OrderByDescending(x => x.VersionDate).First())
                //.ToListAsync();

                //var data = dataContext.EntityDefinitions.AsQueryable()
                //    .GroupBy(y => y.Name)
                //    .Select(z => z.OrderByDescending(x => x.VersionDate).First())
                //    .ToList();
                 
                /// V3
                //var groupByPayments = new BsonDocument
                //      {
                //          { "Name", "$Name" },
                //          { "_id", new BsonDocument { { "$first", "$_id" } } },
                //          { "Identifier", new BsonDocument { { "$first", "$Identifier" } } },
                //          { "VersionDate", new BsonDocument { { "$first", "$VersionDate" } } }
                //      };

                //var sort = Builders<Model.Storage.EntityDefinition>.Sort.Descending(document => document.VersionDate);

                //ProjectionDefinition<BsonDocument> projection = new BsonDocument
                //{
                //    {"name", "$_id"},
                //    {"id", "$Id"},
                //};

                //var statuses = dataContext.EntityDefinitions.Aggregate()
                //    .Sort(sort)
                //    .Group(groupByPayments)
                //    .Project(projection)
                //    .ToList<BsonDocument>();

                /// v4
                var data = dataContext.EntityDefinitions.AsQueryable()
                               .OrderByDescending(e => e.VersionDate)
                               .GroupBy(e => e.Name)
                               .Select(g => new Model.Storage.EntityDefinition
                               {
                                   Id = g.First().Id,
                                   Name = g.Key,
                                   Identifier = g.First().Identifier,
                                   VersionDate = g.First().VersionDate,
                                   DataSourceDefinition = g.First().DataSourceDefinition,
                                   Properties = g.First().Properties,
                               })
                               .ToList();

                return Task.FromResult(entityDefinitionsResult.Succeeded(data));
            }
            catch (Exception e)
            {
                return Task.FromResult(entityDefinitionsResult.Failed(e));
            }
        }

        /// <summary>
        /// Find by name the last version of this entity definition
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<entityDefinitionResult> FindLatestByNameAsync(AppDataContext dataContext, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return entityDefinitionResult.Failed(new ArgumentNullException(nameof(name)));
                }

                var filter = Builders<Model.Storage.EntityDefinition>.Filter.Eq(nameof(Model.Storage.EntityDefinition.Name), name);
                var sort = Builders<Model.Storage.EntityDefinition>.Sort.Descending(document => document.VersionDate);
                var options = new FindOptions<Model.Storage.EntityDefinition>
                {
                    Sort = sort
                };
                var data = await dataContext.EntityDefinitions.FindAsync(filter, options)
                    .ConfigureAwait(false);
                
                if (data == null)
                {
                    entityDefinitionResult.Failed(new Exception("not found."));
                }

                var entity = await data.FirstOrDefaultAsync();
                
                return entityDefinitionResult.Succeeded(entity);
            }
            catch (Exception e)
            {
                return entityDefinitionResult.Failed(e);
            }
        }

        public static async Task<entityDefinitionsResult> FindAllByNameAsync(AppDataContext dataContext, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return entityDefinitionsResult.Failed(new ArgumentNullException(nameof(name)));
                }

                var filter = Builders<Model.Storage.EntityDefinition>.Filter.Eq(nameof(Model.Storage.EntityDefinition.Name), name);
                var sort = Builders<Model.Storage.EntityDefinition>.Sort.Descending(document => document.VersionDate);
                var options = new FindOptions<Model.Storage.EntityDefinition>
                {
                    Sort = sort
                };
                var data = await dataContext.EntityDefinitions.FindAsync(filter, options)
                    .ConfigureAwait(false);

                if (data == null)
                {
                    entityDefinitionsResult.Failed(new Exception("not found."));
                }

                var entity = data.ToList();

                return entityDefinitionsResult.Succeeded(entity);
            }
            catch (Exception e)
            {
                return entityDefinitionsResult.Failed(e);
            }
        }

        /// <summary>
        /// Creates new entity definition
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="entityDefinition"></param>
        /// <returns></returns>
        public static async Task<entityDefinitionResult> AddOneAsync(AppDataContext dataContext, Model.Storage.EntityDefinition entityDefinition)
        {
            try
            {
                if (entityDefinition == null)
                {
                    return entityDefinitionResult.Failed(new ArgumentNullException(nameof(entityDefinition)));
                }

                if (string.IsNullOrEmpty(entityDefinition.Name))
                {
                    return entityDefinitionResult.Failed(new ArgumentNullException(nameof(entityDefinition.Name)));
                }

                // check if the name is already exist, then is not allowed to add as new entity definition
                var existingEntityResult = await FindLatestByNameAsync(dataContext, entityDefinition.Name);

                if (existingEntityResult.IsSuccess && existingEntityResult.Success != null)
                {
                    return entityDefinitionResult.Failed(new Exception($"Definition with the name {entityDefinition.Name} already exists..."));
                }

                entityDefinition.VersionDate = DateTime.Now;
                entityDefinition.Identifier = Guid.NewGuid();

                await dataContext.EntityDefinitions.InsertOneAsync(entityDefinition);

                return entityDefinitionResult.Succeeded(entityDefinition);
            }
            catch (Exception e)
            {
                return entityDefinitionResult.Failed(e);
            }
        }

        /// <summary>
        /// Save new version of the entity definitions
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="entityDefinition"></param>
        /// <returns></returns>
        public static async Task<entityDefinitionResult> UpdateAsync(AppDataContext dataContext, Model.Storage.EntityDefinition entityDefinition)
        {
            try
            {
                if (entityDefinition == null)
                {
                    return entityDefinitionResult.Failed(new ArgumentNullException(nameof(entityDefinition)));
                }

                if (string.IsNullOrEmpty(entityDefinition.Name))
                {
                    return entityDefinitionResult.Failed(new ArgumentNullException(nameof(entityDefinition.Name)));
                }

                // check if the name is already exist, if not, return error... On saving new version, the entity with the name must exists
                var existingEntityResult = await FindLatestByNameAsync(dataContext, entityDefinition.Name);

                if (existingEntityResult.IsFailure || existingEntityResult.Success == null)
                {
                    return entityDefinitionResult.Failed(new Exception($"Definition with the name {entityDefinition.Name} does not exists..."));
                }

                entityDefinition.VersionDate = DateTime.Now;
                entityDefinition.Identifier = Guid.NewGuid();

                await dataContext.EntityDefinitions.InsertOneAsync(entityDefinition);

                return entityDefinitionResult.Succeeded(entityDefinition);
            }
            catch (Exception e)
            {
                return entityDefinitionResult.Failed(e);
            }
        }
    }
}

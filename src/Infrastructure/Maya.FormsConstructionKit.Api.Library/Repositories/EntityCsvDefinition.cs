using MongoDB.Bson;
using MongoDB.Driver;

namespace Maya.FormsConstructionKit.Api.Library.Repositories
{
    public static class EntityCsvDefinition
    {
        public static async Task<entityCsvDefinitionsResult> AllAsync(AppDataContext dataContext)
        {
            try
            {
                var filter = Builders<Model.Storage.EntityCsvDefinition>.Filter.Empty;
                var sort = Builders<Model.Storage.EntityCsvDefinition>.Sort.Descending(nameof(Model.Storage.EntityCsvDefinition.VersionDate));

                var data = await dataContext.EntityCsvDefinitions.FindAsync(filter, new FindOptions<Model.Storage.EntityCsvDefinition, Model.Storage.EntityCsvDefinition>()
                {
                    Sort = sort
                }).ConfigureAwait(false);

                if (data == null)
                {
                    return entityCsvDefinitionsResult.Failed(new Exception("not found."));
                }

                var csvDefinitions = await data.ToListAsync();

                return entityCsvDefinitionsResult.Succeeded(csvDefinitions);
            }
            catch (Exception e)
            {
                return entityCsvDefinitionsResult.Failed(e);
            }
        }

        public static async Task<entityCsvDefinitionsResult> AllForEntityAsync(AppDataContext dataContext, string entityName)
        {
            try
            {
                if (string.IsNullOrEmpty(entityName))
                {
                    return entityCsvDefinitionsResult.Failed(new ArgumentNullException(nameof(entityName)));
                }

                var filter = Builders<Model.Storage.EntityCsvDefinition>.Filter.Eq(nameof(Model.Storage.EntityCsvDefinition.EntityName), entityName);
                var sort = Builders<Model.Storage.EntityCsvDefinition>.Sort.Descending(nameof(Model.Storage.EntityCsvDefinition.VersionDate));

                var data = await dataContext.EntityCsvDefinitions.FindAsync(filter, new FindOptions<Model.Storage.EntityCsvDefinition, Model.Storage.EntityCsvDefinition>()
                {
                    Sort = sort
                }).ConfigureAwait(false);

                if (data == null)
                {
                    return entityCsvDefinitionsResult.Failed(new Exception("not found."));
                }

                var csvDefinitions = await data.ToListAsync();

                return entityCsvDefinitionsResult.Succeeded(csvDefinitions);
            }
            catch (Exception e)
            {
                return entityCsvDefinitionsResult.Failed(e);
            }
        }

        public static async Task<entityCsvDefinitionResult> FindOneById(AppDataContext dataContext, string id)
        {
            try
            {
                var filter = Builders<Model.Storage.EntityCsvDefinition>.Filter.Eq(nameof(Model.Storage.EntityValue.Id), id);
                var data = await dataContext.EntityCsvDefinitions.FindAsync(filter)
                    .ConfigureAwait(false);

                if (data == null)
                {
                    return entityCsvDefinitionResult.Failed(new Exception("not found."));
                }

                var csvDefinitions = await data.ToListAsync();

                var first = csvDefinitions.FirstOrDefault();

                if (first == null)
                {
                    return entityCsvDefinitionResult.Failed(new Exception("not found."));
                }

                return entityCsvDefinitionResult.Succeeded(first);
            }
            catch (Exception e)
            {
                return entityCsvDefinitionResult.Failed(e);
            }
        }

        public static async Task<entityCsvDefinitionResult> AddOneAsync(AppDataContext dataContext, Model.Storage.EntityCsvDefinition entityCsvDefinition)
        {
            try
            {
                if (entityCsvDefinition == null)
                {
                    return entityCsvDefinitionResult.Failed(new ArgumentNullException(nameof(entityCsvDefinition)));
                }

                if (string.IsNullOrEmpty(entityCsvDefinition.EntityName))
                {
                    return entityCsvDefinitionResult.Failed(new ArgumentNullException(nameof(entityCsvDefinition.EntityName)));
                }

                entityCsvDefinition.VersionDate = DateTime.Now;

                await dataContext.EntityCsvDefinitions.InsertOneAsync(entityCsvDefinition);

                return entityCsvDefinitionResult.Succeeded(entityCsvDefinition);
            }
            catch (Exception e)
            {
                return entityCsvDefinitionResult.Failed(e);
            }
        }
    }
}

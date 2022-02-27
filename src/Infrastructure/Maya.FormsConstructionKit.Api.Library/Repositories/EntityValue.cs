using MongoDB.Bson;
using MongoDB.Driver;

namespace Maya.FormsConstructionKit.Api.Library.Repositories
{
    public static class EntityValue
    {
        public static async Task<entityValueResult> AddAsync(AppDataContext dataContext, Model.Storage.EntityValue entityValue)
        {
            try
            {
                if (entityValue == null)
                {
                    return entityValueResult.Failed(new ArgumentNullException(nameof(entityValue)));
                }

                if (string.IsNullOrEmpty(entityValue.Name))
                {
                    return entityValueResult.Failed(new ArgumentException($"Argument {nameof(entityValue.Name)} is requiered"));
                }

                entityValue.VersionDate = DateTime.Now;

                await dataContext.EntityValues.InsertOneAsync(entityValue);

                return entityValueResult.Succeeded(entityValue);
            }
            catch (Exception e)
            {
                return entityValueResult.Failed(e);
            }
        }

        public static async Task<entityValuesResult> FindAllByEntityNameAsync(AppDataContext dataContext, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return entityValuesResult.Failed(new ArgumentNullException(nameof(name)));
                }

                var filter = Builders<Model.Storage.EntityValue>.Filter.Eq(nameof(Model.Storage.EntityValue.Name), name);
                var sort = Builders<BsonDocument>.Sort.Descending(nameof(Model.Storage.EntityValue.VersionDate));

                var data = await dataContext.EntityValues.FindAsync(filter)
                    .ConfigureAwait(false);

                if (data == null)
                {
                    return entityValuesResult.Failed(new Exception("not found."));
                }

                var entityValues = await data.ToListAsync();

                return entityValuesResult.Succeeded(entityValues);
            }
            catch (Exception e)
            {
                return entityValuesResult.Failed(e);
            }
        }

        public static async Task<entityValuesResult> FindAllByPropertyValue(AppDataContext dataContext, string entityName, PropertyValue property)
        {
            try
            {
                //var builder = Builders<Model.Storage.EntityValue>.Filter;

                //Func<PropertyValue, bool> expr = property.Type switch
                //{
                //    ContentType.Bool => v => v.BoolVal == property.BoolVal,
                //    ContentType.DateOnly => v => v.DateVal == property.DateVal,
                //    ContentType.DateTime => v => v.DateTimeVal == property.DateTimeVal,
                //    ContentType.Decimal => v => v.DecimalVal == property.DecimalVal,
                //    ContentType.Float => v => v.FloatVal == property.FloatVal,
                //    ContentType.Int => v => v.IntVal == property.IntVal,
                //    ContentType.Long => v => v.LongVal == property.LongVal,
                //    ContentType.Text => v => v.TextVal == property.TextVal,
                //    _ => throw new NotImplementedException(property.Type.ToString())
                //};

                //var filter = builder.Eq(nameof(Model.Storage.EntityValue.Name), entityName) &
                //    builder.ElemMatch(x => x.Value, v => expr(v) && v.Name == property.Name);
                var matchPropFilter = property.Type switch
                {
                    ContentType.Bool => Builders<Model.Storage.EntityValue>.Filter.ElemMatch(x => x.Value, v => v.BoolVal == property.BoolVal && v.Name == property.Name),
                    ContentType.DateOnly => Builders<Model.Storage.EntityValue>.Filter.ElemMatch(x => x.Value, v => v.DateVal == property.DateVal && v.Name == property.Name),
                    ContentType.DateTime => Builders<Model.Storage.EntityValue>.Filter.ElemMatch(x => x.Value, v => v.DateTimeVal == property.DateTimeVal && v.Name == property.Name),
                    ContentType.Decimal => Builders<Model.Storage.EntityValue>.Filter.ElemMatch(x => x.Value, v => v.DecimalVal == property.DecimalVal && v.Name == property.Name),
                    ContentType.Float => Builders<Model.Storage.EntityValue>.Filter.ElemMatch(x => x.Value, v => v.FloatVal == property.FloatVal && v.Name == property.Name),
                    ContentType.Int => Builders<Model.Storage.EntityValue>.Filter.ElemMatch(x => x.Value, v => v.IntVal == property.IntVal && v.Name == property.Name),
                    ContentType.Long => Builders<Model.Storage.EntityValue>.Filter.ElemMatch(x => x.Value, v => v.LongVal == property.LongVal && v.Name == property.Name),
                    ContentType.Text => Builders<Model.Storage.EntityValue>.Filter.ElemMatch(x => x.Value, v => v.TextVal == property.TextVal && v.Name == property.Name),
                    _ => throw new NotImplementedException(property.Type.ToString())
                };

                var filter = Builders<Model.Storage.EntityValue>.Filter.And(
                        Builders<Model.Storage.EntityValue>.Filter.Eq(nameof(Model.Storage.EntityValue.Name), entityName),
                        matchPropFilter
                    );
                var data = await dataContext.EntityValues.FindAsync(filter)
                     .ConfigureAwait(false);

                if (data == null)
                {
                    return entityValuesResult.Failed(new Exception("not found."));
                }

                var entityValues = await data.ToListAsync();
                return entityValuesResult.Succeeded(entityValues);
            }
            catch (Exception e)
            {
                return entityValuesResult.Failed(e);
            }
        }

        public static async Task<entityValueResult> UpdateAsync(AppDataContext dataContext, Model.Storage.EntityValue entityValue, string originalId)
        {
            try
            {
                if (entityValue == null)
                {
                    return entityValueResult.Failed(new ArgumentNullException(nameof(entityValue)));
                }

                if (string.IsNullOrEmpty(entityValue.Name))
                {
                    return entityValueResult.Failed(new ArgumentException($"Argument {nameof(entityValue.Name)} is requiered"));
                }

                entityValue.VersionDate = DateTime.Now;

                var filter = Builders<Model.Storage.EntityValue>.Filter.Eq(g => g.Id, originalId);

                var result = await dataContext.EntityValues.FindOneAndReplaceAsync(filter, entityValue);

                if (result == null)
                {
                    return entityValueResult.Failed(new ArgumentNullException("Unexpected failure on replacing the value..."));
                }

                return entityValueResult.Succeeded(result);
            }
            catch (Exception e)
            {
                return entityValueResult.Failed(e);
            }
        }
    }
}

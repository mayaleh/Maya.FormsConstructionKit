using System.Reflection;
using Maya.Ext;
using Maya.Ext.Rop;
using Maya.Ext.TaskUtilities;
using Maya.FormsConstructionKit.Api.Library.Mappers.Storage;

namespace Maya.FormsConstructionKit.Api.Library.Services.DataSource
{
    public class StorageDataSourceService : IDataSourceService
    {
        private readonly AppDataContext dataContext;
        private ILogger? logger;

        public StorageDataSourceService(AppDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Result<object, Exception>> Create(Model.Storage.EntityDefinition entityDefinition, object entityValue)
        {
            var actionName = $"{nameof(StorageDataSourceService)}.{nameof(Create)} {{0}}";
            try
            {
                // will ensure existing key property
                // will validate, that unique props have unique value
                // is not validating if is filled
                var values = ObjectValueMapper.MapObjectToValueModel(entityDefinition.Properties, entityValue);

                return await Task.FromResult(ObjectValueMapper.GetObjectKey(entityDefinition, entityValue)) // ensure, key exists
                    .BindAsync(async _ => await this.ValidateUniqueProperties(entityDefinition, values, entityValue))
                    .BindAsync(async _ =>
                    {
                        return await Repositories.EntityValue.AddAsync(this.dataContext,
                        new EntityValue
                        {
                            Name = entityDefinition.Name,
                            Value = values
                        });
                    })
                    .MapAsync(value =>
                    {
                        var objVal = ObjectValueMapper.MapValueModelToObject(value.Value);
                        return Task.FromResult(objVal);
                    })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, "{0}.{1} crash on create {2}", nameof(StorageDataSourceService), nameof(Create), entityDefinition.Name);
                return Result<object, Exception>.Failed(e);
            }
        }

        public Task<Result<Unit, Exception>> Delete(Model.Storage.EntityDefinition entityDefinition, object entityValue)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IEnumerable<object>, Exception>> Read(Model.Storage.EntityDefinition entityDefinition)
        {
            var actionName = $"{nameof(StorageDataSourceService)}.{nameof(Read)} {{0}}";
            try
            {
                return await Repositories.EntityValue.FindAllByEntityNameAsync(this.dataContext, entityDefinition.Name)
                    .MapAsync(values => Task.FromResult(
                            values.Select(x => ObjectValueMapper.MapValueModelToObject(x.Value))
                        ))
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                this.logger?.LogError(e, "{0}.{1} crash on read {2}", nameof(StorageDataSourceService), nameof(Read), entityDefinition.Name);
                return Result<IEnumerable<object>, Exception>.Failed(e);
            }
        }

        public async Task<Result<object, Exception>> Update(Model.Storage.EntityDefinition entityDefinition, object entityValue)
        {
            try
            {
                if (string.IsNullOrEmpty(entityDefinition.Id))
                {
                    return Result<object, Exception>.Failed(new Exception($"The key field on form {entityDefinition.Name} have to be set!"));
                }
                var values = ObjectValueMapper.MapObjectToValueModel(entityDefinition.Properties, entityValue); // convert to key value
                return await this.ValidateUniqueProperties(entityDefinition, values, entityValue, isUpdate: true) // validate unique values
                    .BindAsync(_ =>
                    {
                        return Task.FromResult(ObjectValueMapper.GetObjectKey(entityDefinition, entityValue)); // load the identifier key
                    })
                    .BindAsync(async keyProp =>
                    { // find all by the identifier key and its value
                        return await Repositories.EntityValue.FindAllByPropertyValue(this.dataContext, entityDefinition.Name, keyProp);
                    })
                    .BindAsync(async entityValues =>
                    { // update all found results
                        var tasks = new List<Task<Result<EntityValue, Exception>>>();
                        var valueNew = new EntityValue
                        {
                            Name = entityDefinition.Name,
                            Value = values
                        };
                        foreach (var item in entityValues)
                        {
                            if (string.IsNullOrEmpty(item.Id))
                                continue;

                            valueNew.Id = item.Id; // is immutable
                            tasks.Add(Repositories.EntityValue.UpdateAsync(this.dataContext, valueNew, item.Id));
                        }

                        var result = await TaskExtension.WhenAll(tasks.ToArray());

                        var fails = result.Where(x => x.IsFailure)
                            .Select(x => x.Failure.Message);

                        if (fails.Any())
                        {
                            var errMessage = string.Join(';', fails);
                            return Result<IEnumerable<EntityValue>, Exception>.Failed(new Exception(errMessage));
                        }
                        var success = result.Where(x => x.IsSuccess)
                            .Select(x => x.Success);
                        return Result<IEnumerable<EntityValue>, Exception>.Succeeded(success);
                    })
                    .MapAsync(rowsAffected =>
                    {
                        var row = rowsAffected.FirstOrDefault();
                        var objVal = ObjectValueMapper.MapValueModelToObject(row!.Value); // possible null exception!
                        return Task.FromResult(objVal);
                    });
            }
            catch (Exception e)
            {
                return Result<object, Exception>.Failed(e);
            }
        }

        public IDataSourceService WithLogger(ILogger logger)
        {
            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            this.logger = logger;

            return this;
        }


        private async Task<Result<object, Exception>> ValidateUniqueProperties(EntityDefinition entityDefinition, List<PropertyValue> values, object toReturnVal, bool isUpdate = false)
        {
            var uniqueProps = entityDefinition.Properties.Where(p => p.IsUnique)
                    .ToList();

            if (uniqueProps == null || uniqueProps.Count <= 0)
            {
                return Result<object, Exception>.Succeeded(toReturnVal); // no unique prop settings
            }

            var concurrentValuesErrors = new List<string>();

            foreach (var uniqueProp in uniqueProps)
            {
                var uniqueVal = values.FirstOrDefault(v => v.Name == uniqueProp.Name);
                if (uniqueVal == null)
                {
                    continue; // maybe error - value not found, the objext value is invalid...
                }

                var concurrentValuesResult = await Repositories.EntityValue.FindAllByPropertyValue(this.dataContext, entityDefinition.Name, uniqueVal)
                    .RunWhenSuccessAsync(foundConcurrents =>
                    {
                        if (foundConcurrents != null && foundConcurrents.Any())
                        {
                            if (isUpdate && uniqueVal.Name == entityDefinition.KeyPropertyName)
                            {
                                return Task.CompletedTask; // possibility of updating wrong row if the key property value changed
                            }
                            concurrentValuesErrors.Add($"Concurrent value error for property {uniqueVal.Name}");
                        }

                        return Task.CompletedTask;
                    });
            }

            if (concurrentValuesErrors.Count > 0)
            {
                var errMessage = string.Join(';', concurrentValuesErrors);
                return Result<object, Exception>.Failed(new Exception(errMessage));
            }
            return Result<object, Exception>.Succeeded(toReturnVal);
        }
    }
}

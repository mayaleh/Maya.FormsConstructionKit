
using Maya.FormsConstructionKit.Api.Model.Storage;
using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Storage
{
    public static class EntityDefinitionMapper
    {
        /// <summary>
        /// TODO implement AutoMapper!
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EntityForm Map(this EntityDefinition entity)
        {
            var form = new EntityForm()
            {
                Name = entity.Name,
                Identifier = entity.Identifier,
                VersionDate = entity.VersionDate,
                KeyPropertyName = entity.KeyPropertyName,
                Components = entity.Properties.Select(x => x.Map())
                    .ToList(),
                DataSource = entity.DataSourceDefinition.Map()
            };
            
            return form;
        }
    }
}

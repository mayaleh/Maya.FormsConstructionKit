using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Api.Model.Storage;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Shared.Model
{
    public static class EntityFormMapper
    {
        public static EntityDefinition Map(this Maya.FormsConstructionKit.Shared.Model.EntityForm entity)
        {
            return new EntityDefinition
            {
                Identifier = entity.Identifier,
                Name = entity.Name,
                VersionDate = entity.VersionDate,
                KeyPropertyName = entity.KeyPropertyName,
                Properties = entity.Components.Select(x => ComponentObjMapper.MapObject(x))
                    .ToArray(),
                DataSourceDefinition = entity.DataSource.Map()
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Api.Library.Services.DataSource
{
    public interface IDataSourceService
    {
        IDataSourceService WithLogger(ILogger logger);

        Task<Maya.Ext.Rop.Result<object, System.Exception>> Create(Model.Storage.EntityDefinition entityDefinition, object entityValue);

        Task<Maya.Ext.Rop.Result<System.Collections.Generic.IEnumerable<object>, System.Exception>> Read(Model.Storage.EntityDefinition entityDefinition);

        Task<Maya.Ext.Rop.Result<object, System.Exception>> Update(Model.Storage.EntityDefinition entityDefinition, object entityValue);

        Task<Maya.Ext.Rop.Result<Maya.Ext.Unit, System.Exception>> Delete(Model.Storage.EntityDefinition entityDefinition, object entityValue);
    }
}

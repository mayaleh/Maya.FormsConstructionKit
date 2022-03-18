using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services.Api;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.Services
{
    public interface IApiService
    {
        IEntityFormService EntityForm { get; }

        IValuesService Values { get; }

        ICsvDefinitionService CsvDefinition { get; }

        Task<Result<Shared.Model.AppInfo, Exception>> GetApiInfoAsync();
    }
}

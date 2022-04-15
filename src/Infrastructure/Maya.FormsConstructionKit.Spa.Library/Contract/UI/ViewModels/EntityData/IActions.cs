using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData
{
    public interface IActions
    {
        Task Delete(object name);

        void Edit(object row);
        
        Task Load();

        Task LoadGrid(GridDataProviderRequest<object> request);

        Task Save();

        Task ExportCsv(string csvId);

        void ShowCreateForm();
    }
}

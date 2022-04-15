using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web.Bootstrap;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData
{
    public interface ICommands : IDisposable
    {
        ICommandAsync LoadCommand { get; }
        
        ICommandAsync<GridDataProviderRequest<object>> LoadGridCommand { get; }

        ICommandAsync SaveCommand { get; }

        ICommandAsync<object> DeleteCommand { get; }

        ICommand<object> EditCommand { get; }

        ICommand ShowCreateFormCommand { get; }

        ICommandAsync<string> ExportToCsv { get; }
    }
}

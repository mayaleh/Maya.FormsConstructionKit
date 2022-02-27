using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData
{
    public interface IEntityDataViewModel : IBaseViewModel, IDisposable
    {
        IJSRuntime JSRuntime { get; }

        string EntityName { get; }
        
        FormsConstructionKit.Shared.Model.EntityData EntityData { get; set; }

        Shared.Model.CsvDefinition[] CsvDefinitions { get; set; }

        List<Shared.Model.ComponentValue> RowManaged { get; set; }

        IApiService ApiService { get; }

        INotifyMessage NotifyMessage { get; }
        
        NavigationManager NavigationManager { get; }

        ICommands Commands { get; }

        IActions Actions { get; }

        bool IsEditingItem { get; set; }
    }
}
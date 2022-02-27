using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData;
using Maya.FormsConstructionKit.Spa.Model.UI;
using Maya.FormsConstructionKit.Spa.Model.UI.View;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Maya.FormsConstructionKit.Spa.ViewModels.EntityData
{
    public class EntityDataViewModel : BaseViewModel, IEntityDataViewModel
    {
        public IApiService ApiService { get; }

        public INotifyMessage NotifyMessage { get; }
        public NavigationManager NavigationManager { get; }

        public IActions Actions { get; }
        public ICommands Commands { get; }

        public Maya.FormsConstructionKit.Shared.Model.EntityData EntityData { get; set; } = Maya.FormsConstructionKit.Shared.Model.EntityData.Create();

        public string EntityName { get; }

        public bool IsEditingItem { get; set; } = false;

        public List<ComponentValue> RowManaged { get; set; } = new();
        
        public IJSRuntime JSRuntime { get; }

        public CsvDefinition[] CsvDefinitions { get; set; }

        public EntityDataViewModel(string entityName,
            Action onUiChanged,
            IApiService apiService,
            INotifyMessage notifyMessage,
            NavigationManager navigationManager,
            IJSRuntime jSRuntime) : base(onUiChanged)
        {
            OnIsInit = (v) => { OnUiChanged.Invoke(); };
            OnIsBusy = (v) => { OnUiChanged.Invoke(); };
            EntityName = entityName;
            ApiService = apiService;
            NotifyMessage = notifyMessage;
            NavigationManager = navigationManager;
            JSRuntime = jSRuntime;

            Actions = new Actions(this);
            Commands = new Commands(this);
        }

        public void Dispose()
        {
            if (Commands != null)
            {
                Commands.Dispose();
            }
        }
    }
}

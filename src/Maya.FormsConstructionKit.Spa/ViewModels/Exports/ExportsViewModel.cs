using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.Exports;
using Maya.FormsConstructionKit.Spa.Model.UI;
using Maya.FormsConstructionKit.Spa.Model.UI.View;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.ViewModels.Exports
{
    public class ExportsViewModel : BaseViewModel, IExportsViewModel
    {
        private readonly IApiService apiService;

        private readonly INotifyMessage notifyMessage;
        private readonly NavigationManager navigationManager;

        public List<Maya.FormsConstructionKit.Shared.Model.CsvDefinition> CsvDefinitions { get; private set; } = new();

        public ICommandAsync LoadCommand { get; set; }

        public ICommandAsync<string> DeleteCommand { get; set; }

        public ICommand<string> EditCommand { get; set; }

        public ICommand CreateCommand { get; set; }

        private readonly string entityName;

        public ExportsViewModel(string entityName,
            Action onUiChanged,
            IApiService apiService,
            INotifyMessage notifyMessage,
            NavigationManager navigationManager) : base(onUiChanged)
        {
            this.entityName = entityName;
            OnIsInit = (v) => { OnUiChanged.Invoke(); };
            OnIsBusy = (v) => { OnUiChanged.Invoke(); };
            this.apiService = apiService;
            this.notifyMessage = notifyMessage;
            this.navigationManager = navigationManager;
            LoadCommand = new CommandAsync(this.Load);
            DeleteCommand = new CommandAsync<string>(this.Delete);
            EditCommand = new Command<string>(this.Edit);
            CreateCommand = new Command(this.Create);

            LoadCommand!.OnExecuteChanged += this.LoadCommand_OnExecuteChanged;
            DeleteCommand!.OnExecuteChanged += this.DeleteCommand_OnExecuteChanged;
            EditCommand!.OnExecuteChanged += this.EditCommand_OnExecuteChanged;
            CreateCommand!.OnExecuteChanged += this.CreateCommand_OnExecuteChanged;
        }

        public async Task Init()
        {
            IsInit = true;
            await this.Load()
                .ConfigureAwait(false);

            IsInit = false;
        }

        private void LoadCommand_OnExecuteChanged(object? sender, bool e) => IsBusy = e;
        private void DeleteCommand_OnExecuteChanged(object? sender, bool e) => IsBusy = e;
        private void EditCommand_OnExecuteChanged(object? sender, bool e) => IsBusy = e;
        private void CreateCommand_OnExecuteChanged(object? sender, bool e) => IsBusy = e;

        private void Edit(string name)
        {
            var url = Library.Helper.RouteHelper.ComposePath(UI.PagesName.Export, name);

            this.navigationManager.NavigateTo(url);
        }

        private void Create()
        {
            var url = Library.Helper.RouteHelper.ComposePath(UI.PagesName.Export);

            this.navigationManager.NavigateTo(url);
        }

        private async Task Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                this.notifyMessage?.Error("Empty ID.");
                return;
            }

            var result = await this.apiService.CsvDefinition.DeleteAsync(id)
                .ConfigureAwait(false);

            if (result.IsSuccess == false)
            {
                this.notifyMessage?.Error(result.Failure.Message);
                return;
            }

            this.notifyMessage?.Success($"Successfully deleted.");

            await this.Load()
                .ConfigureAwait(false);
        }

        private async Task Load()
        {
            var result = await this.apiService.CsvDefinition.GetAllForEntityAsync(this.entityName)
                .ConfigureAwait(false);

            if (result.IsFailure)
            {
                Console.WriteLine(result.Failure.Message);
                this.notifyMessage?.Error(result.Failure.Message);
            }

            CsvDefinitions = result.Success?.ToList() ?? new();
        }

        public void Dispose()
        {
            if (LoadCommand != null)
            {
                LoadCommand.OnExecuteChanged -= this.LoadCommand_OnExecuteChanged;
            }
            if (DeleteCommand != null)
            {
                DeleteCommand.OnExecuteChanged -= this.DeleteCommand_OnExecuteChanged;
            }
            if (EditCommand != null)
            {
                EditCommand.OnExecuteChanged -= this.EditCommand_OnExecuteChanged;
            }
            if (CreateCommand != null)
            {
                CreateCommand.OnExecuteChanged -= this.CreateCommand_OnExecuteChanged;
            }
        }
    }
}

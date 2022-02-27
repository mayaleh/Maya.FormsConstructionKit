using Maya.FormsConstructionKit.Spa.Library.Contract.UI;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData;
using Maya.FormsConstructionKit.Spa.Model.UI;

namespace Maya.FormsConstructionKit.Spa.ViewModels.EntityData
{
    public class Commands : ICommands
    {
        private readonly IEntityDataViewModel vm;

        public ICommandAsync LoadCommand { get; private set; }

        public ICommandAsync SaveCommand { get; private set; }

        public ICommandAsync<string> ExportToCsv { get; private set; }

        public ICommandAsync<object> DeleteCommand { get; private set; }

        public ICommand<object> EditCommand { get; private set; }

        public ICommand ShowCreateFormCommand { get; private set; }

        public Commands(IEntityDataViewModel vm)
        {
            this.vm = vm;
            this.InitCommands();
        }

        private void InitCommands()
        {
            LoadCommand = new CommandAsync(this.vm.Actions.Load);
            SaveCommand = new CommandAsync(this.vm.Actions.Save);
            ExportToCsv = new CommandAsync<string>(this.vm.Actions.ExportCsv);
            DeleteCommand = new CommandAsync<object>(this.vm.Actions.Delete);
            EditCommand = new Command<object>(this.vm.Actions.Edit);
            ShowCreateFormCommand = new Command(this.vm.Actions.ShowCreateForm);

            LoadCommand!.OnExecuteChanged += this.LoadCommand_OnExecuteChanged;
            ExportToCsv!.OnExecuteChanged += this.ExportToCsv_OnExecuteChanged;
            DeleteCommand!.OnExecuteChanged += this.DeleteCommand_OnExecuteChanged;
            EditCommand!.OnExecuteChanged += this.EditCommand_OnExecuteChanged;
            ShowCreateFormCommand!.OnExecuteChanged += this.ShowCreateFormCommand_OnExecuteChanged;
        }

        private void LoadCommand_OnExecuteChanged(object? sender, bool e) => this.vm.IsBusy = e;
        private void SaveCommand_OnExecuteChanged(object? sender, bool e) => this.vm.IsBusy = e;
        private void ExportToCsv_OnExecuteChanged(object? sender, bool e) => this.vm.IsBusy = e;
        private void DeleteCommand_OnExecuteChanged(object? sender, bool e) => this.vm.IsBusy = e;
        private void EditCommand_OnExecuteChanged(object? sender, bool e) => this.vm.IsBusy = e;
        private void ShowCreateFormCommand_OnExecuteChanged(object? sender, bool e) => this.vm.IsBusy = e;


        public void Dispose()
        {
            if (LoadCommand != null)
            {
                LoadCommand.OnExecuteChanged -= this.LoadCommand_OnExecuteChanged;
            }
            if (SaveCommand != null)
            {
                SaveCommand.OnExecuteChanged -= this.SaveCommand_OnExecuteChanged;
            }
            if (ExportToCsv != null)
            {
                ExportToCsv.OnExecuteChanged -= this.ExportToCsv_OnExecuteChanged;
            }
            if (DeleteCommand != null)
            {
                DeleteCommand.OnExecuteChanged -= this.DeleteCommand_OnExecuteChanged;
            }
            if (EditCommand != null)
            {
                EditCommand.OnExecuteChanged -= this.EditCommand_OnExecuteChanged;
            }
            if (ShowCreateFormCommand != null)
            {
                ShowCreateFormCommand.OnExecuteChanged -= this.ShowCreateFormCommand_OnExecuteChanged;
            }
        }
    }
}

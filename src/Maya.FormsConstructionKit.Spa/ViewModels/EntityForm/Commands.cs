using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm;
using Maya.FormsConstructionKit.Spa.Model.UI;

namespace Maya.FormsConstructionKit.Spa.ViewModels.EntityForm
{
    public class Commands : ICommands
    {
        private readonly IEntityFormViewModel vm;

        public ICommandAsync SaveCommand { get; private init; }
        
        public ICommand<ComponentAll> EditComponentCommand { get; private init; }
                
        public ICommand CancelEditingComponentCommand { get; private init; }

        public ICommand CompleteEditingComponentCommand { get; private init; }

        public ICommand ShowAddComponentCommand { get; private init; }

        public Commands(IEntityFormViewModel vm)
        {
            this.vm = vm;

            SaveCommand = new CommandAsync(this.vm.Actions.SaveAsync);
            EditComponentCommand = new Command<ComponentAll>(this.vm.Actions.EditComponent);
            CompleteEditingComponentCommand = new Command(this.vm.Actions.CompleteEditingComponent);
            CancelEditingComponentCommand = new Command(this.vm.Actions.CancelEditingComponent);
            ShowAddComponentCommand = new Command(this.vm.Actions.ShowAddComponent);
        }
    }
}

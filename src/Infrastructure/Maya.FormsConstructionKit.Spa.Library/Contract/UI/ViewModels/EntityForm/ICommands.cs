namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm
{
    public interface ICommands
    {
        ICommandAsync SaveCommand { get; }

        ICommand<Shared.Model.ComponentAll> EditComponentCommand { get; }
        
        ICommand ShowAddComponentCommand { get; }

        ICommand CompleteEditingComponentCommand { get; }

        ICommand CancelEditingComponentCommand { get; }
    }
}

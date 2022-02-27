namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForms
{
    public interface IEntityFormsViewModel : IBaseViewModel, IDisposable
    {
        List<Shared.Model.EntityForm> EntityForms { get; }

        ICommandAsync LoadCommand { get; }

        ICommandAsync<string> DeleteCommand { get; }
        
        ICommand<string> EditCommand { get; }

        ICommand CreateCommand { get; }
    }
}

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI
{
    public interface IBaseCommand
    {
        bool CanExecute { get; }

        bool Executing { get; }

        event EventHandler<bool> OnExecuteChanged;
    }
}

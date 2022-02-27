namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI
{
    public interface ICommand<TParam> : IBaseCommand
    {
        void Execute(TParam input);
    }

    public interface ICommand : IBaseCommand
    {
        void Execute();
    }
}

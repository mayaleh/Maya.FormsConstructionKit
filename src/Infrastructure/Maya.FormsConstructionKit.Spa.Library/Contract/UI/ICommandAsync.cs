namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI
{
    public interface ICommandAsync<TParam> : IBaseCommand
    {
        Task Execute(TParam input);
    }

    public interface ICommandAsync : IBaseCommand
    {
        Task Execute();
    }
}

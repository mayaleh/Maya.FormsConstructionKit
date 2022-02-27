namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI
{
    public interface IResultCommandAsync<TParam, TResult> : IBaseCommand
    {
        Task<TResult> Execute(TParam input);
    }
}

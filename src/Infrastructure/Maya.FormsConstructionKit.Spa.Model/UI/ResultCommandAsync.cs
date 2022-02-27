using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.Model.UI
{
    public class ResultCommandAsync<TParam, TResult> : BaseCommand, IResultCommandAsync<TParam, TResult>
    {
        private readonly Func<TParam, Task<TResult>> action;

        ResultCommandAsync() : base()
        {
        }

        public ResultCommandAsync(Func<TParam, Task<TResult>> action) : this()
        {
            this.action = action;
            CanExecute = true;
        }

        public Task<TResult>? Execute(TParam input)
        {
            if (!CanExecute)
            {
                return default;
            }

            return this.action?.Invoke(input);
        }
    }
}

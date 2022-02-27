using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.Model.UI
{
    public class CommandAsync<TParam> : BaseCommand, ICommandAsync<TParam>
    {
        private readonly Func<TParam, Task> action;

        public CommandAsync(Func<TParam, Task> action, IObservable<bool>? onCanExecute = null) : base()
        {
            this.action = action;
            CanExecute = true;

            onCanExecute?.Subscribe((v) =>
            {
                Console.WriteLine("COMMAND onCanExecute change: {0}", v);
                if (Executing) return;

                CanExecute = v;
            });
        }

        public async Task Execute(TParam input)
        {
            if (!CanExecute) return;

            CanExecute = false;
            Executing = true;

            await this.action?.Invoke(input);

            Executing = false;
            CanExecute = true;
        }
    }

    public class CommandAsync : BaseCommand, ICommandAsync
    {
        private readonly Func<Task> action;

        public CommandAsync(Func<Task> action, IObservable<bool>? onCanExecute = null) : base()
        {
            this.action = action;
            CanExecute = true;

            onCanExecute?.Subscribe((v) =>
            {
                Console.WriteLine("COMMAND onCanExecute change: {0}", v);
                if (Executing) return;

                CanExecute = v;
            });
        }

        public async Task Execute()
        {
            if (!CanExecute) return;

            CanExecute = false;
            Executing = true;

            await this.action?.Invoke();

            Executing = false;
            CanExecute = true;
        }
    }
}

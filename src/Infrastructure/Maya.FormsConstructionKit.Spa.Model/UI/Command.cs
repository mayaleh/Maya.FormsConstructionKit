using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.Model.UI
{
    public class Command<TParam> : BaseCommand, ICommand<TParam>
    {
        private readonly Action<TParam> action;
        //readonly Predicate<object> canExecute;

        Command() : base()
        {
        }

        public Command(Action<TParam> action) : this()
        {
            this.action = action;
            CanExecute = true;
        }

        public void Execute(TParam input)
        {
            if (!CanExecute) return;

            CanExecute = false;
            Executing = true;

            this.action?.Invoke(input);

            Executing = false;
            CanExecute = true;
        }
    }

    public class Command : BaseCommand, ICommand
    {
        private readonly Action action;

        Command() : base()
        {
        }

        public Command(Action action) : this()
        {
            this.action = action;
            CanExecute = true;
        }

        public void Execute()
        {
            if (!CanExecute) return;

            CanExecute = false;
            Executing = true;

            this.action?.Invoke();

            Executing = false;
            CanExecute = true;
        }
    }
}

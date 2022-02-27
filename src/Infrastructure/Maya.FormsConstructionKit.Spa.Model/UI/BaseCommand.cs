using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.Model.UI
{
    public abstract class BaseCommand : View.NotifyModel, IBaseCommand
    {
        private bool canExecute;
        private bool executing;

        public bool CanExecute
        {
            get { return this.canExecute; }
            internal set
            {
                this.SetProperty(ref this.canExecute, value);
            }
        }

        public bool Executing
        {
            get { return this.executing; }
            internal set
            {
                this.SetProperty(ref this.executing, value);

                OnExecuteChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<bool>? OnExecuteChanged;
    }
}

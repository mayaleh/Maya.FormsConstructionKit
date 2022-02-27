using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.Model.UI.View
{
    public class BaseViewModel : NotifyModel, INotifyUI, IBaseViewModel
    {
        private bool isInit, isBusy;

        public Action OnUiChanged { get; }
        public Action<bool>? OnIsInit { get; set; }
        public Action<bool>? OnIsBusy { get; set; }

        public bool IsInit
        {
            get { return this.isInit; }
            set
            {
                this.isInit = value;

                OnIsInit?.Invoke(value);
            }
        }

        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;

                OnIsBusy?.Invoke(value);
            }
        }

        public BaseViewModel(Action onUiChanged)
        {
            OnUiChanged = onUiChanged ?? throw new ArgumentNullException(nameof(onUiChanged));
        }
    }
}

using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.Model.UI.View
{
    public class NotifyUI : INotifyUI
    {
        public NotifyUI(Action onUiChanged)
        {
            OnUiChanged = onUiChanged;
        }

        public Action OnUiChanged { get; }
    }
}

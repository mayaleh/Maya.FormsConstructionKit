using Havit.Blazor.Components.Web;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.UI.Models
{
    public class NotifyMessage : INotifyMessage
    {
        public Action<string> Error { get; set; } = null!;

        public Action<string> Warning { get; set; } = null!;

        public Action<string> Info { get; set; } = null!;

        public Action<string> Success { get; set; } = null!;

        public Action<MessengerMessage> Show { get; set; } = null!;
    }
}

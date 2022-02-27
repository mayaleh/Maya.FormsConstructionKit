using Havit.Blazor.Components.Web;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI
{
    public interface INotifyMessage
    {
        Action<string> Error { get; set; }
        Action<string> Warning { get; set; }
        Action<string> Info { get; set; }
        Action<string> Success { get; set; }

        Action<MessengerMessage> Show { get; set; }
    }

    public enum NotifyMessageStatus
    {
        Error,
        Warning,
        Success,
        Info
    }
}

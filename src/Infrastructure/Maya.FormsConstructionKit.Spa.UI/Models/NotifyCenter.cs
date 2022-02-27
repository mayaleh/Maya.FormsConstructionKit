using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.UI.Models
{
    public class NotifyCenter
    {
        private readonly INotifyMessage notifyMessage;
        private readonly IHxMessengerService messengerService;

        public NotifyCenter(INotifyMessage notifyMessage, IHxMessengerService messengerService)
        {
            this.notifyMessage = notifyMessage;
            this.messengerService = messengerService;

            this.notifyMessage.Error = (message) => this.Error(message);
            this.notifyMessage.Warning = (message) => this.Warning(message);
            this.notifyMessage.Info = (message) => this.Info(message);
            this.notifyMessage.Success = (message) => this.Success(message);
            this.notifyMessage.Show = (model) => this.Show(model);
        }

        public void Error(string text) => this.Show(text, NotifyMessageStatus.Error);
        public void Warning(string text) => this.Show(text, NotifyMessageStatus.Warning);
        public void Info(string text) => this.Show(text, NotifyMessageStatus.Info);
        public void Success(string text) => this.Show(text, NotifyMessageStatus.Success);

        public void Show(MessengerMessage notificationModel)
        {
            if (notificationModel == null) return;

            this.messengerService.AddMessage(notificationModel);
        }
        
        private void Show(string text, NotifyMessageStatus status, string? title = null)
        {
            if (this.messengerService == null) return;

            if (string.IsNullOrEmpty(title))
            {
                title = status.ToString();
            }

            if (status == NotifyMessageStatus.Error)
            {
                this.messengerService.AddError(title, text);
                return;
            }

            if (status == NotifyMessageStatus.Warning)
            {
                this.messengerService.AddWarning(title, text);
                return;
            }

            if (status == NotifyMessageStatus.Success)
            {
                this.messengerService.AddMessage(new MessengerMessage
                {
                    Title = title,
                    Text = text,
                    Icon = BootstrapIcon.CheckCircle,
                    CssClass = "hx-messenger-information text-success",
                    AutohideDelay = 5000,
                });
                return;
            }

            if (status == NotifyMessageStatus.Info)
            {
                this.messengerService.AddInformation(title, text);
                return;
            }

            this.messengerService.AddInformation(title, text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Havit.Blazor.Components.Web;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Core.Container
{
    public partial class ContentContainer
    {
        [Inject]
        protected IHxMessengerService MessengerService { get; set; } = null!;

        [Parameter]
        public bool IsLoading { get; set; }

        [Parameter]
        public bool IsBusy { get; set; }

        [Parameter]
        public RenderFragment? Content { get; set; }

        [Parameter]
        public INotifyMessage NotifyMessage { get; set; } = null!;

        private Models.NotifyCenter? notifyCenter;

        protected override void OnParametersSet()
        {
            if (NotifyMessage != null)
            {
                this.notifyCenter = new Models.NotifyCenter(NotifyMessage, MessengerService);
            }

            base.OnParametersSet();
        }
    }
}

using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForms;
using Maya.FormsConstructionKit.Spa.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.Pages
{
    [Authorize]
    public partial class EntityForms
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IApiService ApiService { get; set; } = null!;

        private IEntityFormsViewModel? vm;

        public NotifyMessage NotifyMessage { get; set; } = new NotifyMessage();

        protected override void OnInitialized()
        {
            this.vm = new ViewModels.EntityForms.EntityFormsViewModel(() => this.StateHasChanged(),
                ApiService!,
                NotifyMessage,
                NavigationManager);
        }
    }
}

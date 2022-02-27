using Havit.Blazor.Components.Web;
using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm;
using Maya.FormsConstructionKit.Spa.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.Pages
{
    [Authorize]
    public partial class EntityForm
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        public IApiService ApiService { get; set; } = null!;

        private IEntityFormViewModel? vm;

        public NotifyMessage NotifyMessage { get; set; } = new NotifyMessage();

        [Parameter]
        public string? Name { get; set; }

        protected override async void OnInitialized()
        {
            this.vm = new ViewModels.EntityForm.EntityFormViewModel(NotifyMessage,
                ApiService!,
                () => this.StateHasChanged());

            await this.vm.Init(Name);
        }
    }
}

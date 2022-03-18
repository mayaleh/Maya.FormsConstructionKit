using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.Exports;
using Maya.FormsConstructionKit.Spa.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;


namespace Maya.FormsConstructionKit.Spa.Pages
{
    [Authorize]
    public partial class EntityExports
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IApiService ApiService { get; set; } = null!;

        private IExportsViewModel? vm;

        [Parameter]
        public string EntityName { get; set; } = null!;

        public NotifyMessage NotifyMessage { get; set; } = new NotifyMessage();

        protected override async Task OnInitializedAsync()
        {
            this.vm = new ViewModels.Exports.ExportsViewModel(EntityName,
                () => this.StateHasChanged(),
                ApiService!,
                NotifyMessage,
                NavigationManager);

            await this.vm.Init();
        }
    }
}

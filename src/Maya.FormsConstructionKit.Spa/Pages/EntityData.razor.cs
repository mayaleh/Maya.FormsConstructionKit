using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData;
using Maya.FormsConstructionKit.Spa.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Maya.FormsConstructionKit.Spa.Pages
{
    [Authorize]
    public partial class EntityData : IDisposable
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public IApiService ApiService { get; set; } = null!;

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;

        private IEntityDataViewModel? vm;

        [Parameter]
        public string EntityName { get; set; } = null!;

        public NotifyMessage NotifyMessage { get; set; } = new NotifyMessage();

        //protected override async Task OnParametersSetAsync()
        //{
            
        //}

        protected override async Task OnParametersSetAsync()
        {
            if (this.vm == null)
            {
                this.vm = new ViewModels.EntityData.EntityDataViewModel(EntityName,
                () => this.StateHasChanged(),
                ApiService!,
                NotifyMessage,
                NavigationManager,
                JSRuntime);
                await base.OnParametersSetAsync();
            }

            await this.vm.OnEntityEventChanged(EntityName);

            await base.OnParametersSetAsync();
        }

        public void Dispose()
        {
            if (this.vm != null)
            {
                this.vm.Dispose();
            }
        }

        //protected override void OnInitialized()
        //{
        //    Console.WriteLine(EntityName);
        //    this.vm = new ViewModels.EntityData.EntityDataViewModel(EntityName,
        //        () => this.StateHasChanged(),
        //        ApiService!,
        //        NotifyMessage,
        //        NavigationManager,
        //        JSRuntime);
        //}
    }
}

using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData;
using Maya.FormsConstructionKit.Spa.Library.Helper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Views.EntityData
{
    public partial class View
    {
        [CascadingParameter(Name = nameof(IEntityDataViewModel))]
        public IEntityDataViewModel ViewModel { get; set; } = null!;

        [Inject]
        public IJSRuntime JSRuntime { get; set; } = null!;

        private HxModal rowValueFormModal = null!;
        private HxGrid<object> dataGird = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                ViewModel.EntityNameChanged = this.dataGird.RefreshDataAsync;
            }
            base.OnAfterRender(firstRender);
        }

        private async Task<GridDataProviderResult<object>> ProcessingDataProvider(GridDataProviderRequest<object> request)
        {
            await ViewModel!.Commands.LoadCommand.Execute();
            return request.ApplyTo(ViewModel.EntityData.Data ?? new List<object>());
        }

        private async Task HandleEditRow(object row)
        {
            ViewModel.Commands.EditCommand.Execute(row);
            await this.rowValueFormModal.ShowAsync();
        }

        private async Task HandleAddRow()
        {
            ViewModel!.Commands.ShowCreateFormCommand.Execute();
            await this.rowValueFormModal.ShowAsync();
        }

        private async Task HandleCancelEditingRow()
        {
            //ViewModel.Commands.CancelEditingCommand.Execute();
            await this.rowValueFormModal.HideAsync();
        }

        private async Task HandleSaveRow()
        {
            await ViewModel.Commands.SaveCommand.Execute();
            await this.rowValueFormModal.HideAsync();
            await this.dataGird.RefreshDataAsync();
        }
    }
}

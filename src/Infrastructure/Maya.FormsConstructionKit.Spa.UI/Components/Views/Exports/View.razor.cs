using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.Exports;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Views.Exports
{
    public partial class View
    {
        [CascadingParameter(Name = nameof(IExportsViewModel))]
        public IExportsViewModel ViewModel { get; set; } = null!;

        private async Task<GridDataProviderResult<Maya.FormsConstructionKit.Shared.Model.CsvDefinition>> ProcessingDataProvider(GridDataProviderRequest<Maya.FormsConstructionKit.Shared.Model.CsvDefinition> request)
        {
            await ViewModel!.LoadCommand.Execute();
            return request.ApplyTo(ViewModel.CsvDefinitions);
        }
    }
}

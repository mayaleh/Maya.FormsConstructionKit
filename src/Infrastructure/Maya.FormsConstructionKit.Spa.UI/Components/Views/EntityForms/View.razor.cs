using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForms;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Views.EntityForms
{
    public partial class View
    {
        [CascadingParameter(Name = nameof(IEntityFormsViewModel))]
        public IEntityFormsViewModel ViewModel { get; set; } = null!;

        private async Task<GridDataProviderResult<Maya.FormsConstructionKit.Shared.Model.EntityForm>> ProcessingDataProvider(GridDataProviderRequest<Maya.FormsConstructionKit.Shared.Model.EntityForm> request)
        {
            await ViewModel!.LoadCommand.Execute();
            return request.ApplyTo(ViewModel.EntityForms);
        }
    }
}

using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm;
using Maya.FormsConstructionKit.Spa.Library.Helper;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Views.EntityForm
{
    public partial class ComponentsView
    {
        [CascadingParameter(Name = nameof(IEntityFormViewModel))]
        public IEntityFormViewModel ViewModel { get; set; } = null!;

        private HxModal componentEditingForm = null!;
        private HxGrid<Maya.FormsConstructionKit.Shared.Model.ComponentAll> componentsGird = null!;

        private async Task HandleEditComponent(Shared.Model.ComponentAll component)
        {
            ViewModel.Commands.EditComponentCommand.Execute(component);
            await this.componentEditingForm.ShowAsync();
        }

        private async Task HandleAddComponent()
        {
            ViewModel.Commands.ShowAddComponentCommand.Execute();
            await this.componentEditingForm.ShowAsync();
        }

        private async Task HandleCancelEditingComponent()
        {
            ViewModel.Commands.CancelEditingComponentCommand.Execute();
            await this.componentEditingForm.HideAsync();
        }

        private async Task HandleCompleteEditingComponent()
        {
            ViewModel.Commands.CompleteEditingComponentCommand.Execute();
            await this.componentEditingForm.HideAsync();
            await this.componentsGird.RefreshDataAsync();
        }

        private Task<GridDataProviderResult<Maya.FormsConstructionKit.Shared.Model.ComponentAll>> ProcessingDataProvider(GridDataProviderRequest<Maya.FormsConstructionKit.Shared.Model.ComponentAll> request)
        {
            //var data = ViewModel.EntityForm.Components.Select(x => x.MapObject<Shared.Model.ComponentAll>())
            //    .ToList();
            return Task.FromResult(request.ApplyTo(ViewModel.EntityForm.Components));
        }
    }
}

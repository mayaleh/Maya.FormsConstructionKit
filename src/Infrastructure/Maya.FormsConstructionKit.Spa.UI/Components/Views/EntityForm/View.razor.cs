using System.Text.Json;
using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Views.EntityForm
{
    public partial class View
    {
        [CascadingParameter(Name = nameof(IEntityFormViewModel))]
        public IEntityFormViewModel ViewModel { get; set; } = null!;
        
        private async Task ValidSubmitHandler()
        {
            await ViewModel.Commands.SaveCommand.Execute()
                .ConfigureAwait(false);
        }
    }
}

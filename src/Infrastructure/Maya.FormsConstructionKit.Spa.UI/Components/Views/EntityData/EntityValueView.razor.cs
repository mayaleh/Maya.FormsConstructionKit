using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData;
using Maya.FormsConstructionKit.Spa.Library.Extensions;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Views.EntityData
{
    public partial class EntityValueView
    {
        [CascadingParameter(Name = nameof(IEntityDataViewModel))]
        public IEntityDataViewModel ViewModel { get; set; } = null!;
    }
}

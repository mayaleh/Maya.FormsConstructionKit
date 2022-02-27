using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Shared.Model.Extention;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm;
using Maya.FormsConstructionKit.Spa.Model;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Views.EntityForm
{
    public partial class ComponentView
    {
        [CascadingParameter(Name = nameof(IEntityFormViewModel))]
        public IEntityFormViewModel ViewModel { get; set; } = null!;
                
        private ComponentKind ComponentKindSelected { get => ViewModel.ComponentEdit.ComponentKind; set => this.ComponentKindChanged(value); }

        private ComponentBuilderForm componentBuilderForm = new ComponentBuilderForm();

        private void ComponentKindChanged(ComponentKind value)
        {
            ViewModel.ComponentEdit.ValueKind = value.ValueKinds()
                .FirstOrDefault();
            ViewModel.ComponentEdit.ComponentKind = value;
        }

        private static string PiDigits(int? places)
        {
            if (places == null)
                return Math.PI.ToString("0.00");
            var format = "0.";
            for (int i = 0; i < places; i++)
            {
                format += "0";
            }
            return Math.PI.ToString(format);
        }
    }
}

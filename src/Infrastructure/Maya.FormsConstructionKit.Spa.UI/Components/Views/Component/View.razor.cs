using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Shared.Model.Extention;
using Maya.FormsConstructionKit.Spa.Model;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Views.Component
{
    public partial class View
    {
        private ComponentAll componentConfiguration = new ComponentAll();

        private ComponentKind ComponentKindSelected { get => this.componentConfiguration.ComponentKind; set => this.ComponentKindChanged(value); }

        private ComponentBuilderForm componentBuilderForm = new ComponentBuilderForm();

        private void ComponentKindChanged(ComponentKind value)
        {
            this.componentConfiguration.ValueKind = value.ValueKinds()
                .FirstOrDefault();
            this.componentConfiguration.ComponentKind = value;
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

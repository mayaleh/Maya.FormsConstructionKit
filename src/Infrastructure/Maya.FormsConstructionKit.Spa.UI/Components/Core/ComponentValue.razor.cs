using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Core
{
    public partial class ComponentValue
    {
        [Parameter]
        public Shared.Model.ComponentAll Definition { get => this.component; set => this.component = value; }

        [Parameter]
        public Shared.Model.ComponentValue Data { get; set; } = null!;

        private Shared.Model.ComponentAll component = null!;
    }
}

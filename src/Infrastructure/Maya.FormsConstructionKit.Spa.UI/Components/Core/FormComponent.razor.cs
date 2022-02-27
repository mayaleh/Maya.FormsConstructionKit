using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Core
{
    public partial class FormComponent
    {
        [Parameter]
        public Shared.Model.ComponentAll Definition { get => this.component; set => this.component = value; }

        [Parameter]
        public Shared.Model.ComponentValue Data { get; set; } = null!;

        [Parameter]
        public EventCallback<Shared.Model.ComponentValue> DataChanged { get; set; }

        private Shared.Model.ComponentAll component = null!;

        private bool isComponentSupported = false;

        private string valueStr
        {
            get => (string)Data.Value;
            set
            {
                Data.Value = value;
                DataChanged.InvokeAsync();
            }
        }

        private bool valueBool
        {
            get => (bool)Data.Value;
            set
            {
                Data.Value = value;
                DataChanged.InvokeAsync();
            }
        }

        private int valueInt
        {
            get => (int)Data.Value;
            set
            {
                Data.Value = value;
                DataChanged.InvokeAsync();
            }
        }

        private long valueLong
        {
            get => (long)Data.Value;
            set
            {
                Data.Value = value;
                DataChanged.InvokeAsync();
            }
        }

        private float valueFloat
        {
            get => (float)Data.Value;
            set
            {
                Data.Value = value;
                DataChanged.InvokeAsync();
            }
        }

        private decimal valueDecimal
        {
            get => (decimal)Data.Value;
            set
            {
                Data.Value = value;
                DataChanged.InvokeAsync();
            }
        }

        private DateOnly valueDate
        {
            get => (DateOnly)Data.Value;
            set
            {
                Data.Value = value;
                DataChanged.InvokeAsync();
            }
        }
        
        private DateTime valueDateTime
        {
            get => (DateTime)Data.Value;
            set
            {
                Data.Value = value;
                DataChanged.InvokeAsync();
            }
        }
    }
}

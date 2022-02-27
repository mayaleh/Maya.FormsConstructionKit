using Maya.FormsConstructionKit.Shared.Model;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.UI.Components.Core
{
    public partial class ComponentControl
    {
        [Parameter]
        public ComponentAll ComponentDefinition { get; set; } = null!;

        [Parameter]
        public object BoundValue { get => ComponentDefinition.ValueKind switch
            {
                ValueKind.Bool => this.boolVal,
                ValueKind.Decimal => this.decimalVal,
                ValueKind.Float => this.floatVal,
                ValueKind.DateOnly => this.dateOnlyVal,
                ValueKind.DateTime => this.dateTimeVal,
                ValueKind.Int => this.intVal,
                ValueKind.Long => this.longVal,
                ValueKind.Text => this.textVal,
                _ => throw new NotImplementedException(ComponentDefinition.ValueKind.ToString())
            };
            set => this.SetVal(value);
            }
        
        private void SetVal(object newVal)
        {
            if (ComponentDefinition.ValueKind == ValueKind.Bool)
            {
                this.boolVal = (bool)newVal;
                return;
            }
            if (ComponentDefinition.ValueKind == ValueKind.Decimal)
            {
                this.decimalVal = (decimal)newVal;
                return;
            }
            if (ComponentDefinition.ValueKind == ValueKind.Float)
            {
                this.floatVal = (float)newVal;
                return;
            }
            if (ComponentDefinition.ValueKind == ValueKind.DateOnly)
            {
                this.dateOnlyVal = (DateOnly)newVal;
                return;
            }
            if (ComponentDefinition.ValueKind == ValueKind.DateTime)
            {
                this.dateTimeVal = (DateTime)newVal;
                return;
            }
            if (ComponentDefinition.ValueKind == ValueKind.Int)
            {
                this.intVal = (int)newVal;
                return;
            }
            if (ComponentDefinition.ValueKind == ValueKind.Long)
            {
                this.longVal = (long)newVal;
                return;
            }
            
            if (ComponentDefinition.ValueKind == ValueKind.Text)
            {
                this.textVal = (string)newVal;
                return;
            }

            throw new NotImplementedException(ComponentDefinition.ValueKind.ToString());
        }
        
        private bool boolVal;
        private decimal decimalVal;
        private float floatVal;
        private DateOnly dateOnlyVal;
        private DateTime dateTimeVal;
        private int intVal;
        private long longVal;
        private string textVal;
    }
}

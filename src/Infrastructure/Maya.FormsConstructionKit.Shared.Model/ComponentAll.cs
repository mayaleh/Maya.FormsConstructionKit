namespace Maya.FormsConstructionKit.Shared.Model
{
    public class ComponentAll : Component, ICloneable
    {
        public string? Placeholder { get; set; }
        
        public bool IsUnique { get; set; }

        public int? Decimals { get; set; }

        public object Clone()
        {
            return (ComponentAll)this.MemberwiseClone();
        }

        public void Bind(ComponentAll bindTo)
        {
            bindTo.ComponentKind = ComponentKind;
            bindTo.Decimals = Decimals;
            bindTo.Hint = Hint;
            bindTo.Label = Label;
            bindTo.Name = Name;
            bindTo.Placeholder = Placeholder;
            bindTo.ValueKind = ValueKind;
            bindTo.IsUnique = IsUnique;
        }
    }
}
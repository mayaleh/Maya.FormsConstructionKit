namespace Maya.FormsConstructionKit.Shared.Model
{
    public static class NumericBoxIntComponent
    {
        public static ComponentAll Create()
        {
            return new ComponentAll
            {
                ComponentKind = ComponentKind.NumericBox,
                ValueKind = ValueKind.Int,
            };
        }

        public static ComponentAll Set(this ComponentAll component)
        {
            component.ComponentKind = ComponentKind.NumericBox;
            component.ValueKind = ValueKind.Int;
            return component;
        }
    }
}
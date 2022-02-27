namespace Maya.FormsConstructionKit.Shared.Model
{
    public static class NumericBoxFloatComponent
    {
        public static ComponentAll Create()
        {
            return new ComponentAll
            {
                ComponentKind = ComponentKind.NumericBox,
                ValueKind = ValueKind.Float,
            };
        }

        public static ComponentAll Set(this ComponentAll component)
        {
            component.ComponentKind = ComponentKind.NumericBox;
            component.ValueKind = ValueKind.Float;
            return component;
        }
    }
}
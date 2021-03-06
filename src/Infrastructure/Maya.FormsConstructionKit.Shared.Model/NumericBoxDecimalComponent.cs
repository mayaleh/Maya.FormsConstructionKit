namespace Maya.FormsConstructionKit.Shared.Model
{
    public static class NumericBoxDecimalComponent
    {
        public static ComponentAll Create()
        {
            return new ComponentAll
            {
                ComponentKind = ComponentKind.NumericBox,
                ValueKind = ValueKind.Decimal,
            };
        }

        public static ComponentAll Set(this ComponentAll component)
        {
            component.ComponentKind = ComponentKind.NumericBox;
            component.ValueKind = ValueKind.Decimal;
            return component;
        }
    }
}
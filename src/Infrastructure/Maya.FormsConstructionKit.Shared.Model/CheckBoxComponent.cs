namespace Maya.FormsConstructionKit.Shared.Model
{
    public static class CheckBoxComponent
    {
        public static ComponentAll Create()
        {
            return new ComponentAll
            {
                ComponentKind = ComponentKind.CheckBox,
                ValueKind = ValueKind.Bool,
            };
        }

        public static ComponentAll Set(this ComponentAll component)
        {
            component.ComponentKind = ComponentKind.CheckBox;
            component.ValueKind = ValueKind.Bool;
            return component;
        }
    }
}
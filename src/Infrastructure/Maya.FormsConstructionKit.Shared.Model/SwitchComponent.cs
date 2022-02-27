namespace Maya.FormsConstructionKit.Shared.Model
{
    public static class SwitchComponent
    {
        public static ComponentAll Create()
        {
            return new ComponentAll
            {
                ComponentKind = ComponentKind.Switch,
                ValueKind = ValueKind.Bool,
            };
        }

        public static ComponentAll Set(this ComponentAll component)
        {
            component.ComponentKind = ComponentKind.Switch;
            component.ValueKind = ValueKind.Bool;
            return component;
        }
    }
}
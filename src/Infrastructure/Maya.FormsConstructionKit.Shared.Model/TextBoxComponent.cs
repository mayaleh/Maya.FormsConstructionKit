namespace Maya.FormsConstructionKit.Shared.Model
{
    public static class TextBoxComponent
    {
        public static ComponentAll Create()
        {
            return new ComponentAll
            {
                ComponentKind = ComponentKind.TextBox,
                ValueKind = ValueKind.Text,
            };
        }

        public static ComponentAll Set(this ComponentAll component)
        {
            component.ComponentKind = ComponentKind.TextBox;
            component.ValueKind = ValueKind.Text;
            return component;
        }
    }
}
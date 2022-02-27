namespace Maya.FormsConstructionKit.Shared.Model
{
    public static class TextAreaComponent
    {
        public static ComponentAll Create()
        {
            return new ComponentAll
            {
                ComponentKind = ComponentKind.TextArea,
                ValueKind = ValueKind.Text,
            };
        }
        public static ComponentAll Set(this ComponentAll component)
        {
            component.ComponentKind = ComponentKind.TextArea;
            component.ValueKind = ValueKind.Text;
            return component;
        }
    }
}
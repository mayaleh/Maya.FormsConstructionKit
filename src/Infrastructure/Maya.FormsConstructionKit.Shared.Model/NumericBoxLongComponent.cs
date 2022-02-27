namespace Maya.FormsConstructionKit.Shared.Model
{
    public class NumericBoxLongComponent
    {
        public static ComponentAll Create()
        {
            return new ComponentAll
            {
                ComponentKind = ComponentKind.NumericBox,
                ValueKind = ValueKind.Long,
            };
        }
    }
}
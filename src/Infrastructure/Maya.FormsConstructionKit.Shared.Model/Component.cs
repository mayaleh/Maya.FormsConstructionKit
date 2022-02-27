namespace Maya.FormsConstructionKit.Shared.Model
{
    public class Component
    {
        public string Name { get; set; } = null!;

        public string Label { get; set; } = null!;

        public string? Hint { get; set; }

        public ComponentKind ComponentKind { get; set; }

        public ValueKind ValueKind { get; set; }
    }

    public class Component<T> : ComponentAll
    {
        public T? Value { get; set; }
    }

}
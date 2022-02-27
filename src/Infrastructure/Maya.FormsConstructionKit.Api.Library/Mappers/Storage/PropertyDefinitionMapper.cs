
using Maya.FormsConstructionKit.Api.Model.Storage;
using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Storage
{
    public static class PropertyDefinitionMapper
    {
        public static ComponentAll Map(this PropertyDefinition property)
        {
            return property.Kind switch
            {
                Kind.CheckBox => property.ToCheckBox(),
                Kind.NumericBox => property.ToNumericBox(),
                Kind.Switch => property.ToSwitch(),
                Kind.TextArea => property.ToTextArea(),
                Kind.TextBox => property.ToTextBox(),
                _ => throw new NotImplementedException(property.Kind.ToString())
            };
        }

        private static ComponentAll ToCheckBox(this PropertyDefinition property)
        {
            var c = CheckBoxComponent.Create();
            c.Label = property.Label;
            c.Hint = property.Hint;
            c.Name = property.Name;
            return c;
        }

        private static ComponentAll ToNumericBox(this PropertyDefinition property)
        {
            var c = property.ContentType switch
            {
                ContentType.Decimal => NumericBoxDecimalComponent.Create(),
                ContentType.Float => NumericBoxFloatComponent.Create(),
                ContentType.Int => NumericBoxIntComponent.Create(),
                ContentType.Long => NumericBoxLongComponent.Create(),
                _ => throw new NotImplementedException(property.ContentType.ToString())
            };

            c.Label = property.Label;
            c.Hint = property.Hint;
            c.Name = property.Name;
            c.Decimals = property.Decimals;

            return c;
        }

        private static ComponentAll ToSwitch(this PropertyDefinition property)
        {
            var c = SwitchComponent.Create();

            c.Label = property.Label;
            c.Hint = property.Hint;
            c.Name = property.Name;

            return c;
        }

        private static ComponentAll ToTextArea(this PropertyDefinition property)
        {
            var c = TextAreaComponent.Create();

            c.Label = property.Label;
            c.Hint = property.Hint;
            c.Name = property.Name;
            c.Placeholder = property.Placeholder;

            return c;
        }
        
        private static ComponentAll ToTextBox(this PropertyDefinition property)
        {
            var c = TextBoxComponent.Create();

            c.Label = property.Label;
            c.Hint = property.Hint;
            c.Name = property.Name;
            c.Placeholder = property.Placeholder;
            c.IsUnique = property.IsUnique;

            return c;
        }
    }
}

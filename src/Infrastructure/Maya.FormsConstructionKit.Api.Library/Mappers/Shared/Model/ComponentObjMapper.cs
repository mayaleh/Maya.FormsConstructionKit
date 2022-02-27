using System.Text.Json;
using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Shared.Model
{
    public static class ComponentObjMapper
    {
        private static JsonSerializerOptions GetJsonOption()
        {
            return new JsonSerializerOptions(JsonSerializerDefaults.Web) { PropertyNameCaseInsensitive = true };
        }

        public static PropertyDefinition MapObject(this ComponentAll componentObj)
        {
            try
            {
                var component = componentObj;
                return component!.ComponentKind switch
                {
                    ComponentKind.CheckBox => component!.ToCheckBox(),
                    ComponentKind.NumericBox => component.ToNumericBoxObject(),
                    ComponentKind.Switch => component!.ToSwitch(),
                    ComponentKind.TextArea => component!.ToTextArea(),
                    ComponentKind.TextBox => component!.ToTextBox(),
                    _ => throw new NotImplementedException(component.ComponentKind.ToString())
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static PropertyDefinition ToCheckBox(this ComponentAll component)
        {
            var checkCompo = component;
            return new PropertyDefinition
            {
                ContentType = ContentType.Bool,
                Hint = checkCompo!.Hint,
                Kind = Kind.CheckBox,
                Label = checkCompo.Label,
                Name = checkCompo.Name,
            };
        }

        private static PropertyDefinition ToSwitch(this ComponentAll component)
        {
            var checkCompo = component;
            return new PropertyDefinition
            {
                ContentType = ContentType.Bool,
                Hint = checkCompo!.Hint,
                Kind = Kind.Switch,
                Label = checkCompo.Label,
                Name = checkCompo.Name,
            };
        }

        private static PropertyDefinition ToTextArea(this ComponentAll component)
        {
            var checkCompo = component;
            return new PropertyDefinition
            {
                ContentType = ContentType.Text,
                Hint = checkCompo!.Hint,
                Kind = Kind.TextArea,
                Label = checkCompo.Label,
                Name = checkCompo.Name,
                Placeholder = checkCompo.Placeholder
            };
        }

        private static PropertyDefinition ToTextBox(this ComponentAll component)
        {
            var txtCompo = component;
            return new PropertyDefinition
            {
                ContentType = ContentType.Text,
                Hint = txtCompo!.Hint,
                Kind = Kind.TextBox,
                Label = txtCompo.Label,
                Name = txtCompo.Name,
                Placeholder = txtCompo.Placeholder,
                IsUnique = txtCompo.IsUnique
            };
        }

        #region NumericBox mapping
        private static PropertyDefinition ToNumericBoxObject(this ComponentAll component)
        {
            return component.ValueKind switch
            {
                ValueKind.Float => component!.ToNumericFloat(),
                ValueKind.Decimal => component!.ToNumericDecimal(),
                ValueKind.Int => component!.ToNumericInt(),
                ValueKind.Long => component!.ToNumericLong(),
                _ => throw new NotImplementedException(component.ValueKind.ToString())
            };
        }

        private static PropertyDefinition ToNumericFloat(this ComponentAll component)
        {
            var checkCompo = component;
            return new PropertyDefinition
            {
                ContentType = ContentType.Float,
                Hint = checkCompo!.Hint,
                Kind = Kind.NumericBox,
                Label = checkCompo.Label,
                Name = checkCompo.Name,
                Decimals = checkCompo.Decimals,
            };
        }

        private static PropertyDefinition ToNumericDecimal(this ComponentAll component)
        {
            var checkCompo = component;
            return new PropertyDefinition
            {
                ContentType = ContentType.Decimal,
                Hint = checkCompo!.Hint,
                Kind = Kind.NumericBox,
                Label = checkCompo.Label,
                Name = checkCompo.Name,
                Decimals = checkCompo.Decimals,
            };
        }

        private static PropertyDefinition ToNumericInt(this ComponentAll component)
        {
            var checkCompo = component;
            return new PropertyDefinition
            {
                ContentType = ContentType.Int,
                Hint = checkCompo!.Hint,
                Kind = Kind.NumericBox,
                Label = checkCompo.Label,
                Name = checkCompo.Name,
            };
        }

        private static PropertyDefinition ToNumericLong(this ComponentAll component)
        {
            var checkCompo = component;
            return new PropertyDefinition
            {
                ContentType = ContentType.Long,
                Hint = checkCompo!.Hint,
                Kind = Kind.NumericBox,
                Label = checkCompo.Label,
                Name = checkCompo.Name,
            };
        }
        #endregion

    }
}

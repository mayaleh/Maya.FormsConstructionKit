using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Shared.Model.Extention
{
    public static class ComponentExtention
    {
        public static ValueKind[] ValueKinds(this Component component)
        {
            return component.ComponentKind.ValueKinds();
        }

        public static ValueKind[] ValueKinds(this ComponentKind kind)
        {
            return kind switch
            {
                ComponentKind.CheckBox => new[] { ValueKind.Bool },
                ComponentKind.NumericBox => new[] { ValueKind.Int, ValueKind.Long, ValueKind.Float, ValueKind.Decimal },
                ComponentKind.Switch => new[] { ValueKind.Bool },
                ComponentKind.TextArea => new[] { ValueKind.Text },
                ComponentKind.TextBox => new[] { ValueKind.Text },
                _ => throw new NotImplementedException(),
            };
        }

        public static object DefaultValue(this ValueKind kind)
        {
            return kind switch
            {
                ValueKind.Text => string.Empty,
                ValueKind.Bool => default(bool),
                ValueKind.Decimal => default(decimal),
                ValueKind.Int => default(int),
                ValueKind.Long => default(long),
                ValueKind.Float => default(float),
                ValueKind.DateOnly => default(DateOnly),
                ValueKind.DateTime => default(DateTime),
                _ => throw new NotImplementedException(),
            };
        }

        // get component value
    }
}

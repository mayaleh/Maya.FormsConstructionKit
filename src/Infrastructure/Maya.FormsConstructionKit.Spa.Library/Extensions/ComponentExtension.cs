using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Shared.Model.Extention;

namespace Maya.FormsConstructionKit.Spa.Library.Extensions
{
    public static class ComponentExtension
    {
        public static string GetValueStr(this Shared.Model.ComponentAll component, object values)
        {
            if (values == null)
            {
                return string.Empty;
            }

            if (values is System.Text.Json.JsonElement)
            {
                var jsonEl = (System.Text.Json.JsonElement)values;
                var val = jsonEl.EnumerateObject()
                    .FirstOrDefault(x => x.Name.Equals(component.Name, StringComparison.Ordinal));

                if (val.Value.ValueKind is System.Text.Json.JsonValueKind.Undefined)
                {
                    return "Undefined";
                }

                var type = component.ValueKind;

                if (type == Shared.Model.ValueKind.Bool)
                {
                    return val.Value.GetBoolean() ? "YES" : "NO";
                }

                if (type == Shared.Model.ValueKind.DateTime)
                {
                    return val.Value.GetDateTime()
                        .ToString();
                }

                if (type == Shared.Model.ValueKind.DateOnly)
                {
                    return DateOnly.FromDateTime(val.Value.GetDateTime())
                        .ToString();
                }

                if (type == Shared.Model.ValueKind.Decimal)
                {
                    return val.Value.GetDecimal()
                        .ToString();
                }

                if (type == Shared.Model.ValueKind.Float)
                {
                    var dd = val.Value.JsonElToFloat();
                    return dd.ToString();
                }

                if (type == Shared.Model.ValueKind.Int)
                {
                    return val.Value.GetInt32()
                        .ToString();
                }

                if (type == Shared.Model.ValueKind.Long)
                {
                    return val.Value.GetInt64()
                           .ToString();
                }

                if (type == Shared.Model.ValueKind.Text)
                {
                    return val.Value.GetString() ?? string.Empty;
                }
            }

            return "Unsupported type";
        }

        private static float JsonElToFloat(this System.Text.Json.JsonElement jsonElement)
        {
            var dd = jsonElement.GetDouble();
            return Convert.ToSingle(dd);
        }

        // toto pouzij pro sestaveni manage formulare pro update nebo add data
        public static IEnumerable<Shared.Model.ComponentValue> GetComponentValues(this IEnumerable<Shared.Model.ComponentAll> components, object values)
        {
            if (values is not System.Text.Json.JsonElement)
            {
                throw new ArgumentException(nameof(values));
            }

            var jsonEl = (System.Text.Json.JsonElement)values;
            var vals = jsonEl.EnumerateObject();
            foreach (var component in components)
            {
                var val = vals.FirstOrDefault(x => x.Name.Equals(component.Name, StringComparison.Ordinal));

                if (val.Value.ValueKind is System.Text.Json.JsonValueKind.Undefined)
                {
                    yield return new Shared.Model.ComponentValue() {
                        Name = component.Name,
                        Value = component.ValueKind.DefaultValue()
                    };
                    continue;
                }

                yield return new Shared.Model.ComponentValue()
                {
                    Name = component.Name,
                    Value = component.ValueKind switch
                    {
                        Shared.Model.ValueKind.Bool => val!.Value!.GetBoolean(),
                        Shared.Model.ValueKind.Float => val!.Value!.JsonElToFloat(),
                        Shared.Model.ValueKind.Decimal => val!.Value!.GetDecimal(),
                        Shared.Model.ValueKind.Long => val!.Value!.GetInt64(),
                        Shared.Model.ValueKind.Int => val!.Value!.GetInt32(),
                        Shared.Model.ValueKind.Text => val!.Value!.GetString() ?? string.Empty,
                        Shared.Model.ValueKind.DateOnly => DateOnly.FromDateTime(val!.Value!.GetDateTime()),
                        Shared.Model.ValueKind.DateTime => val!.Value!.GetDateTime(),
                        _ => throw new NotImplementedException(component.ValueKind.ToString())
                    },
                };
            }
        }
    }
}

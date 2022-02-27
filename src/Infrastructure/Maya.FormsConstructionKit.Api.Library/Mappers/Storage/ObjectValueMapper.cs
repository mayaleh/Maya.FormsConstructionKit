using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Maya.Ext.Rop;

namespace Maya.FormsConstructionKit.Api.Library.Mappers.Storage
{
    public static class ObjectValueMapper
    {
        public static List<PropertyValue> MapObjectToValueModel(IEnumerable<PropertyDefinition> properties, object obj)
        {
            var type = obj.GetType();
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (obj is System.Text.Json.JsonElement)
            {
                var jsonEl = (System.Text.Json.JsonElement)obj;
                return jsonEl.EnumerateObject()
                    .Select(x => {
                        var definition = properties.First(p => x.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase));
                        return GetJsonPropVal(x, definition.Name, definition.ContentType);
                    }).ToList();
            }

            var props = type.GetProperties();
            if (props is null)
                throw new ArgumentNullException(nameof(props));

            var data = new List<PropertyValue>();

            for (int i = 0; i < props.Length; i++)
            {
                var prop = props![i];
                var definition = properties.FirstOrDefault(x => x.Name.Equals(prop.Name, StringComparison.OrdinalIgnoreCase));

                if (definition is null)
                    continue;

                var propVal = GetPropVal(definition.Name, definition.ContentType, prop, obj);

                data.Add(propVal);
            }

            return data;
        }

        public static Result<PropertyValue, Exception> GetObjectKey(Model.Storage.EntityDefinition entityDefinition, object entityValue)
        {
            try
            {
                if (string.IsNullOrEmpty(entityDefinition.KeyPropertyName))
                    return Result<PropertyValue, Exception>.Failed(new ArgumentNullException(nameof(entityDefinition.KeyPropertyName)));

                if (entityValue is not System.Text.Json.JsonElement)
                    return Result<PropertyValue, Exception>.Failed(new ArgumentException($"Unsupported object type!"));

                var keyProp = entityDefinition.Properties.FirstOrDefault(x => x.Name == entityDefinition.KeyPropertyName);

                if (keyProp == null)
                    return Result<PropertyValue, Exception>.Failed(new ArgumentException($"The key property does not exists in components definitions"));

                var jsonEl = (System.Text.Json.JsonElement)entityValue;
                var jsonProp = jsonEl.EnumerateObject().FirstOrDefault(x => x.Name == keyProp.Name);

                var propVal = ObjectValueMapper.GetJsonPropVal(jsonProp, keyProp.Name, keyProp.ContentType);
                return Result<PropertyValue, Exception>.Succeeded(propVal);
            }
            catch (Exception e)
            {
                return Result<PropertyValue, Exception>.Failed(e);
            }
        }

        private static PropertyValue GetJsonPropVal(System.Text.Json.JsonProperty obj, string name, ContentType type)
        {
            var val = new PropertyValue
            {
                Name = name,
                Type = type
            };

            if (type == ContentType.Bool)
            {
                val.BoolVal = obj.Value.GetBoolean();
                return val;
            }

            if (type == ContentType.DateTime)
            {
                val.DateTimeVal = obj.Value.GetDateTime();
                return val;
            }

            if (type == ContentType.DateOnly)
            {
                val.DateVal = DateOnly.FromDateTime(obj.Value.GetDateTime());
                return val;
            }

            if (type == ContentType.Decimal)
            {
                val.DecimalVal = obj.Value.GetDecimal();
                return val;
            }

            if (type == ContentType.Float)
            {
                var dd = obj.Value.GetDouble();
                val.FloatVal = Convert.ToSingle(dd);
                return val;
            }

            if (type == ContentType.Int)
            {
                val.IntVal = obj.Value.GetInt32();
                return val;
            }

            if (type == ContentType.Long)
            {
                val.LongVal = obj.Value.GetInt64();
                return val;
            }

            if (type == ContentType.Text)
            {
                val.TextVal = obj.Value.GetString();
                return val;
            }

            throw new NotImplementedException($"Property '{name}' content type: {type}");
        }

        private static PropertyValue GetPropVal(string name, ContentType type, PropertyInfo prop, object obj)
        {
            var val = new PropertyValue
            {
                Name = name,
                Type = type
            };

            if (type == ContentType.Bool)
            {
                val.BoolVal = (bool)prop!.GetValue(obj, null);
                return val;
            }

            if (type == ContentType.DateTime)
            {
                val.DateTimeVal = (DateTime)prop!.GetValue(obj, null);
                return val;
            }

            if (type == ContentType.DateOnly)
            {
                val.DateVal = (DateOnly)prop!.GetValue(obj, null);
                return val;
            }

            if (type == ContentType.Decimal)
            {
                val.DecimalVal = (decimal)prop!.GetValue(obj, null);
                return val;
            }

            if (type == ContentType.Float)
            {
                val.FloatVal = (float)prop!.GetValue(obj, null);
                return val;
            }

            if (type == ContentType.Int)
            {
                val.IntVal = (int)prop!.GetValue(obj, null);
                return val;
            }

            if (type == ContentType.Long)
            {
                val.LongVal = (long)prop!.GetValue(obj, null);
                return val;
            }

            if (type == ContentType.Text)
            {
                val.TextVal = (string)prop!.GetValue(obj, null);
                return val;
            }

            throw new NotImplementedException($"Property '{name}' content type: {type}");
        }


        public static object MapValueModelToObject(List<PropertyValue> propertyValues)
        {
            var d = new Dictionary<string, object>();

            foreach (var propertyValue in propertyValues)
            {
                d.Add(propertyValue.Name, propertyValue.Type switch
                {
                    ContentType.Bool => propertyValue.BoolVal.GetValueOrDefault(),
                    ContentType.DateOnly => propertyValue.DateVal.GetValueOrDefault(),
                    ContentType.DateTime => propertyValue.DateTimeVal.GetValueOrDefault(),
                    ContentType.Decimal => propertyValue.DecimalVal.GetValueOrDefault(),
                    ContentType.Float => propertyValue.FloatVal.GetValueOrDefault(),
                    ContentType.Int => propertyValue.IntVal.GetValueOrDefault(),
                    ContentType.Long => propertyValue.LongVal.GetValueOrDefault(),
                    ContentType.Text => propertyValue.TextVal ?? string.Empty,
                    _ => throw new NotImplementedException($"Property '{propertyValue.Name}' content type: {propertyValue.Type}")
                });
            }

            return d;
        }

        public static Result<List<dynamic>, Exception> MapObjectDictionaryToDynamicList(IEnumerable<object> data)
        {
            try
            {
                var records = data.Select(x =>
                {
                    var propVal = x as Dictionary<string, object>;
                    dynamic row = new ExpandoObject();
                    foreach (var (name, val) in propVal)
                    {
                        row[name] = val;
                    }
                    return row;
                }).ToList();
                return Result<List<dynamic>, Exception>.Succeeded(records);
            }
            catch (Exception e)
            {
                return Result<List<dynamic>, Exception>.Failed(e);
            }
        }

        public static Result<List<Dictionary<string, object>>, Exception> MapObjectDictionaryToCsvCols(IEnumerable<object> data, List<CsvColumn> csvColumns)
        {
            try
            {
                var records = data.Select(x =>
                {
                    var propVal = x as Dictionary<string, object>;
                    var row = new Dictionary<string, object>();
                    foreach (var (name, val) in propVal)
                    {
                        var mapCol = csvColumns.FirstOrDefault(z => z.PropertyName == name);
                        if (mapCol != null)
                        {
                            row.Add(mapCol.Column, val);
                        }
                    }
                    return row;
                }).ToList();
                return Result<List<Dictionary<string, object>>, Exception>.Succeeded(records);
            }
            catch (Exception e)
            {
                return Result<List<Dictionary<string, object>>, Exception>.Failed(e);
            }
        }
    }
}

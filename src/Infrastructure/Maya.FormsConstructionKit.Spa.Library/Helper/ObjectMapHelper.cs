using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Spa.Library.Helper
{
    public static class ObjectMapHelper
    {
        public static TResult? MapObject<TResult>(this object obj)
        {
            try
            {
                var json = JsonSerializer.Serialize(obj);
                return JsonSerializer.Deserialize<TResult>(json, JsonOptions.Options);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static object? GetPropValue(this object src, string propName)
        {
            return src.GetType()
                ?.GetProperty(propName)
                ?.GetValue(src, null);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Shared.Model
{
    public class ComponentValue
    {
        /// <summary>
        /// Component name
        /// </summary>
        public string Name { get; set; } = null!;


        public object Value { get; set; }
    }

    public class ComponentValue<T>
    {
        /// <summary>
        /// Component name
        /// </summary>
        public string Name { get; set; } = null!;


        public T Value { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Api.Model.Storage
{
    public class PropertyDefinition
    {
        public string Name { get; set; } = null!;

        public string Label { get; set; } = null!;

        public string? Hint { get; set; }

        public string? Description { get; set; }

        public string? Placeholder { get; set; }

        public int? Decimals { get; set; }

        public bool IsUnique { get; set; }

        public Kind Kind { get; set; }

        public ContentType ContentType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Shared.Model
{
    public class CsvDefinition
    {
        public string? Id { get; set; }

        /// <summary>
        /// Date of the entity modification or creation
        /// </summary>
        public DateTime VersionDate { get; set; }

        /// <summary>
        /// An unique entity name
        /// </summary>
        public string EntityName { get; set; } = null!;

        public string Name { get; set; } = null!;

        public bool SanitizeForInjection { get; set; }

        public string Delimiter { get; set; } = null!;

        public List<CsvColumn> Columns { get; set; } = null!;
    }
}

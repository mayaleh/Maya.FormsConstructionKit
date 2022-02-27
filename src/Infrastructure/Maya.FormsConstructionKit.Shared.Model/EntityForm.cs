using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Shared.Model
{
    public class EntityForm
    {
        public EntityForm()
        {
            Components = new();
            DataSource = new();
        }
        /// <summary>
        /// Date of the entity modification or creation
        /// </summary>
        public DateTime VersionDate { get; set; }

        /// <summary>
        /// An unique entity name
        /// </summary>
        [Required(ErrorMessage = "The name is requeired!")]
        public string Name { get; set; } = null!;
        
        /// <summary>
        /// Name of the property represented as identifier
        /// </summary>
        [Required(ErrorMessage = "The key property is requeired!")]
        public string KeyPropertyName { get; set; } = null!;

        /// <summary>
        /// Internal identifier. Same for all entity versions
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Properties
        /// </summary>
        public List<ComponentAll> Components { get; set; } = null!;

        public DataSource DataSource { get; set; } = null!;
    }
}

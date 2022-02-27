using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Shared.Model
{
    public class EntityData
    {
        public EntityForm EntityForm { get; set; } = null!;

        public IEnumerable<object> Data { get; set; } = null!;

        public static EntityData Create()
        {
            return new EntityData
            {
                EntityForm = new EntityForm
                {
                    Components = new(),
                    DataSource = new(),
                },
                Data = new List<object>()
            };
        }
    }
}

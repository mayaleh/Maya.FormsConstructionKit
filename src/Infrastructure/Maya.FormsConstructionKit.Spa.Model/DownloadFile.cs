using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Spa.Model
{
    public class DownloadFile
    {
        public string Name { get; init; } = null!;
        public byte[] Content { get; init; } = null!;
        public string ContentType { get; init; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.Settings
{
    public interface IApiService
    {
        string Endpoint { get; set; }

        string[] Scopes { get; set; }
    }
}

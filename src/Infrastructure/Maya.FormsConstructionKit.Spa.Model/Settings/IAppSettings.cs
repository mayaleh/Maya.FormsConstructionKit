using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Spa.Model.Settings
{
    public interface IAppSettings
    {
        ApiService ApiService { get; set; }

        OAuthentication OAuthentication { get; set; }
    }
}

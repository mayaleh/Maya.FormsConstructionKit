using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Spa.Library.Contract.Settings;

namespace Maya.FormsConstructionKit.Spa.Model.Settings
{
    public class OAuthentication : IOAuthentication
    {
        public string Authority { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string PostLogoutRedirectUri { get; set; } = null!;
        public string RedirectUri { get; set; } = null!;
        public string ResponseType { get; set; } = null!;
    }
}

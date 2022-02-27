using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.Settings
{
    public interface IOAuthentication
    {
        string Authority { get; set; }
        string ClientId { get; set; }
        string PostLogoutRedirectUri { get; set; }
        string RedirectUri { get; set; }
        string ResponseType { get; set; }
    }
}

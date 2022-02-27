using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Api.Model.Settings
{
    public class OAuthentication
    {
        public string Audience { get; set; } = null!;

        public string Issuer { get; set; } = null!;

        /// <summary>
        /// Issuer + .well-known/jwks.json
        /// </summary>
        public string IssuerJwksUri { get; set; } = null!;

        public bool RequireHttpsMetadata { get; set; } = true;

        public bool RequireSignedTokens { get; set; } = true;
        
        public bool RequireExpirationTime { get; set; } = true;
        
        public bool ValidateIssuer { get; set; } = true;
        
        public bool ValidateAudience { get; set; } = true;
        
        public bool ValidateIssuerSigningKey { get; set; } = true;
    }
}

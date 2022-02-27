using Maya.FormsConstructionKit.Spa.Model.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Maya.FormsConstructionKit.Spa
{
    public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public ApiAuthorizationMessageHandler(IAccessTokenProvider provider,
               NavigationManager navigation,
               IAppSettings appSettings) : base(provider, navigation)
        {
            //this.ConfigureHandler(authorizedUrls: new[] { appSettings.OAuthentication.Authority,  appSettings.ApiService.Endpoint });
            this.ConfigureHandler(
                authorizedUrls: new[] { appSettings.ApiService.Endpoint, appSettings.OAuthentication.Authority },
                scopes: appSettings.ApiService.Scopes);
            //var authorizedUrls = new[] { this.appSettings.ApiService.Endpoint };
            //var scopes = new[] { "openid", "profile" };
            //var returnUrl = appSettings.OAuthentication.??;

            //this.ConfigureHandler(authorizedUrls: authorizedUrls, scopes, returnUrl: "https://localhost:7112/authentication/login-callback");
        }
    }
}

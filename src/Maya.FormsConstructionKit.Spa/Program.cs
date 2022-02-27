using Maya.FormsConstructionKit.Spa;
using Maya.FormsConstructionKit.Spa.Model.Settings;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Havit.Blazor.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Spa.Library.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.Settings;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#maya-app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var appSettings = builder.Configuration.Get<AppSettings>();
builder.Services.Configure<AppSettings>(a => a = appSettings);
builder.Services.AddTransient<IAppSettings, AppSettings>(sp => appSettings);

builder.Services.AddScoped<ApiAuthorizationMessageHandler>();

builder.Services.AddHttpClient<Maya.FormsConstructionKit.Spa.Library.Contract.Services.IApiService, Maya.FormsConstructionKit.Spa.Library.Services.ApiService>(nameof(Maya.FormsConstructionKit.Spa.Library.Contract.Services.IApiService), cl =>
    {
        cl.BaseAddress = new Uri(appSettings.ApiService.Endpoint);
    })
    .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();

builder.Services.AddScoped(sp =>
    sp.GetService<IHttpClientFactory>()!
        .CreateClient(nameof(Maya.FormsConstructionKit.Spa.Library.Contract.Services.IApiService))
    );

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("OAuthentication", options.ProviderOptions);
});

builder.Services.AddHxServices();
builder.Services.AddHxMessenger();
builder.Services.AddHxMessageBoxHost();

await builder.Build().RunAsync();

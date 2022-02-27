using Maya.FormsConstructionKit.Spa.Library.Contract.Settings;

namespace Maya.FormsConstructionKit.Spa.Model.Settings
{
    public class ApiService : IApiService
    {
        public string Endpoint { get; set; } = null!;
        public string[] Scopes { get; set; } = null!;
    }
}

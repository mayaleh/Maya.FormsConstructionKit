using Maya.FormsConstructionKit.Spa.Library.Contract.Settings;

namespace Maya.FormsConstructionKit.Spa.Model.Settings
{
    public class AppSettings : IAppSettings
    {
        public OAuthentication OAuthentication { get; set; } = null!;

        public ApiService ApiService { get; set; } = null!;
    }
}

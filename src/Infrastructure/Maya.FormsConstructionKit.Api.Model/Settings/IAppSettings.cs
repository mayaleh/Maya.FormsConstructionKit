namespace Maya.FormsConstructionKit.Api.Model.Settings
{
    public interface IAppSettings
    {
        MongoDataSource MongoDataSource { get; set; }
        OAuthentication OAuthentication { get; set; }
        string[] AllowedOrigins { get; set; }
    }
}
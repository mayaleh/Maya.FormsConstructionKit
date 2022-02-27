using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.Services.Api
{
    public interface IValuesService
    {
        Task<Result<object, Exception>> AddAsync(string entityName, object value);
        
        Task<Result<object, Exception>> UpdateAsync(string entityName, object value);
        
        Task<Result<EntityData, Exception>> GetAllAsync(string entityName);
        
        Task<Result<DownloadFile, Exception>> GetCsvAsync(string entityName, string csvId);
    }
}
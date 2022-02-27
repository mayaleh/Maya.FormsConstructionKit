using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.Services.Api
{
    public interface ICsvDefinitionService
    {
        Task<Result<CsvDefinition, Exception>> AddAsync(CsvDefinition csvDefinition);

        Task<Result<CsvDefinition[], Exception>> GetAllAsync();

        Task<Result<CsvDefinition[], Exception>> GetAllForEntityAsync(string entityName);
    }
}

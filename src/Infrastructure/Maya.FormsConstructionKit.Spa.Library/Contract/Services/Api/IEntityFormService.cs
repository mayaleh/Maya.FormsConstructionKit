namespace Maya.FormsConstructionKit.Spa.Library.Contract.Services.Api
{
    public interface IEntityFormService
    {
        Task<Result<Shared.Model.EntityForm, Exception>> AddAsync(Shared.Model.EntityForm entityForm);
        
        Task<Result<Shared.Model.EntityForm, Exception>> UdpateAsync(Shared.Model.EntityForm entityForm);
        
        Task<Result<Ext.Unit, Exception>> DeleteAsync(string name);

        Task<Result<string[], Exception>> GetAvailableAsync();

        Task<Result<Shared.Model.EntityForm[], Exception>> GetAllAsync();

        Task<Result<Shared.Model.EntityForm, Exception>> GetLatestOneAsync(string name);

        Task<Result<Shared.Model.EntityForm[], Exception>> GetOneVersionsAsync(string name);

    }
}

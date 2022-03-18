using Maya.FormsConstructionKit.Shared.Model;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.Exports
{
    public interface IExportsViewModel : IBaseViewModel, IDisposable
    {
        ICommand CreateCommand { get; set; }
        
        List<CsvDefinition> CsvDefinitions { get; }
        
        ICommandAsync<string> DeleteCommand { get; set; }
        
        ICommand<string> EditCommand { get; set; }
        
        ICommandAsync LoadCommand { get; set; }

        Task Init();
    }
}

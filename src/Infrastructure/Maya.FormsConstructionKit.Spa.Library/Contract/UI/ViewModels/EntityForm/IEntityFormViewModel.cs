using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm
{
    public interface IEntityFormViewModel : IBaseViewModel, IDisposable
    {
        INotifyMessage NotifyMessage { get; }

        IApiService ApiService { get; }

        // todo as observable and on any change, notify for unsaved changed
        Shared.Model.EntityForm EntityForm { get; set; } 

        Shared.Model.ComponentAll ComponentBeforeEdit { get; set; }
        
        Shared.Model.ComponentAll ComponentEdit { get; set; }

        bool IsComponentEditVisable { get; set; }

        bool IsCreatingNew { get; }

        bool IsComponentExisting { get; set; }

        ICommands Commands { get; }

        IActions Actions { get; }

        Task Init(string name);
    }
}

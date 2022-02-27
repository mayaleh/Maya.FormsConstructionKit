using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm
{
    public interface IActions
    {
        Task SaveAsync();

        void EditComponent(Shared.Model.ComponentAll component);
        
        void ShowAddComponent();

        void CompleteEditingComponent();

        void CancelEditingComponent();
    }
}

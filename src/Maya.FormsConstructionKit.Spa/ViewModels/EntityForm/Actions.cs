using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm;

namespace Maya.FormsConstructionKit.Spa.ViewModels.EntityForm
{
    public class Actions : IActions
    {
        private readonly IEntityFormViewModel vm;

        public Actions(IEntityFormViewModel vm)
        {
            this.vm = vm;
        }

        public void CancelEditingComponent()
        {
            this.vm.ComponentBeforeEdit.Bind(this.vm.ComponentEdit);
            this.vm.IsComponentEditVisable = false;
        }

        public void CompleteEditingComponent()
        {
            Console.WriteLine($"{nameof(CompleteEditingComponent)} IsComponentExisting {this.vm.IsComponentExisting}");

            if (this.vm.IsComponentExisting == false)
            {
                this.vm.EntityForm.Components.Add(this.vm.ComponentEdit);
            }

            this.vm.IsComponentEditVisable = false;
        }

        public void EditComponent(ComponentAll component)
        {
            this.vm.ComponentBeforeEdit = (ComponentAll)component.Clone();
            this.vm.ComponentEdit = component;
            this.vm.IsComponentExisting = true;
            this.vm.IsComponentEditVisable = true;
        }

        public void ShowAddComponent()
        {
            this.EditComponent(new ComponentAll());
            this.vm.IsComponentExisting = false;
        }

        public async Task SaveAsync()
        {
            this.vm.IsBusy = true;
            this.vm.NotifyMessage.Info("Saving changes...");

            var result = this.vm.IsCreatingNew ?
                await this.vm.ApiService.EntityForm.AddAsync(this.vm.EntityForm)
                    .ConfigureAwait(false) :
                await this.vm.ApiService.EntityForm.UdpateAsync(this.vm.EntityForm)
                    .ConfigureAwait(false);
            
            if (result.IsFailure)
            {
                this.vm.NotifyMessage.Error($"Failled to save the data: {result.Failure.Message}");
                this.vm.IsBusy = false;
                return;
            }

            this.vm.NotifyMessage.Success("Data saved succecssfully. Refreshing the page...");
            
            await this.vm.Init(this.vm.EntityForm.Name)
                .ConfigureAwait(false);

            this.vm.IsBusy = false;
        }
    }
}

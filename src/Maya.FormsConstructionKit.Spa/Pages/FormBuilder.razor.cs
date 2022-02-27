using Havit.Blazor.Components.Web.Bootstrap;
using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Model;

namespace Maya.FormsConstructionKit.Spa.Pages
{
    public partial class FormBuilder
    {
        public FormBuilder()
        {
            this.ComponentList = new List<Component>();
        }

        private Task<GridDataProviderResult<Component>> ClientSideProcessingDataProvider(GridDataProviderRequest<Component> request)
        {
            return Task.FromResult(request.ApplyTo(this.ComponentList));
        }

        private void RemoveComponent(string name)
        {
            var componentToRemove = this.ComponentList.FirstOrDefault(x => x.Name == name);
            if (componentToRemove != null)
                this.ComponentList?.Remove(componentToRemove);
        }

        private void RemoveComponent(Component component)
        {
            if (component != null)
                this.ComponentList?.Remove(component);
        }

        private void EditComponent(Component component)
        {
            // TBD
        }

        private void PreviewComponent(Component component)
        {
            // TBD
        }

        private readonly List<Component> ComponentList;
    }
}

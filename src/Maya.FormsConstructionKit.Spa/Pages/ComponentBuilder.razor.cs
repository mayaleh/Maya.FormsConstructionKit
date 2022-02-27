using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Shared.Model.Extention;
using Maya.FormsConstructionKit.Spa.Model;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.Pages
{
    public partial class ComponentBuilder
    {
        [Parameter]
        public string FormId { get; set; } = null!;

        public ComponentBuilder()
        {
            this.ComponentList = new List<ComponentAll>();
        }

        private void AppedComponent()
        {
            var existingComponent = this.ComponentList.FirstOrDefault(x => x.Name == this.componentConfiguration.Name);
            if (existingComponent != null)
            {
                //Error: Componnent name must be unique and must match the property name of the 3th party system
                return;
            }

            if (string.IsNullOrEmpty(this.componentConfiguration.Name))
            {
                //error Name is always required becasue it is the entity property name
                return;
            }

            // set component kind specific properties from this.componentBuilderForm
            ComponentAll appendComponent = this.componentConfiguration.ComponentKind switch
            {
                ComponentKind.TextBox => TextBoxComponent.Set(new ComponentAll() { Placeholder = this.componentBuilderForm.Placeholder }),
                ComponentKind.TextArea => TextAreaComponent.Set(new ComponentAll() { Placeholder = this.componentBuilderForm.Placeholder }),
                ComponentKind.CheckBox => TextAreaComponent.Set(new ComponentAll()),
                ComponentKind.NumericBox => (this.componentConfiguration.ValueKind switch
                {
                    ValueKind.Int => TextAreaComponent.Set(new ComponentAll()),
                    ValueKind.Float => TextAreaComponent.Set(new ComponentAll() { Decimals = this.componentBuilderForm.Decimals }),
                    ValueKind.Decimal => TextAreaComponent.Set(new ComponentAll() { Decimals = this.componentBuilderForm.Decimals }),
                    _ => throw new NotImplementedException()
                }),
                ComponentKind.Switch => TextAreaComponent.Set(new ComponentAll()),
                _ => throw new NotImplementedException()
            };

            // set common properties this.componentConfiguration
            // appendComponent.ComponentKind is setted on instancing specific Component
            // appendComponent.ValueKind is setted on instancing specific Component
            appendComponent.Hint = this.componentConfiguration.Hint;
            appendComponent.Label = this.componentConfiguration.Label;
            appendComponent.Name = this.componentConfiguration.Name;

            this.ComponentList.Add(appendComponent);

            // reset the form
            this.componentConfiguration = new ComponentAll();
            this.componentBuilderForm = new ComponentBuilderForm();
        }

        private void ComponentKindChanged(ComponentKind value)
        {
            this.componentConfiguration.ValueKind = value.ValueKinds()
                .FirstOrDefault();
            this.componentConfiguration.ComponentKind = value;
        }

        private static string PiDigits(int? places)
        {
            if (places == null)
                return Math.PI.ToString("0.00");
            var format = "0.";
            for (int i = 0; i < places; i++)
            {
                format += "0";
            }
            return Math.PI.ToString(format);
        }

        private ComponentAll componentConfiguration = new ComponentAll();
        
        private ComponentKind ComponentKindSelected { get => this.componentConfiguration.ComponentKind; set => this.ComponentKindChanged(value); }

        private ComponentBuilderForm componentBuilderForm = new ComponentBuilderForm();
        
        private readonly List<ComponentAll> ComponentList;
    }
}

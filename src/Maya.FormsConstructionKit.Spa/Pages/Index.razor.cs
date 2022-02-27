using System.ComponentModel.DataAnnotations;
using Maya.Ext.Func.Rop;
using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.Pages
{
    //[Authorize]
    public partial class Index
    {
        [Inject]
        public IApiService ApiService { get; set; } = null!;

        Maya.FormsConstructionKit.Shared.Model.AppInfo? appInfo;

        public Index()
        {
            this.ComponentList = InitTestComponents();
            this.EntityData = GetData();
        }

        protected override async Task OnInitializedAsync()
        {
             await ApiService.GetApiInfoAsync()
                .MatchSuccessAsync(x =>
                {
                    this.appInfo = x;
                    return Task.FromResult(Ext.Unit.Default);
                });
        }

        private static List<ComponentAll> InitTestComponents()
        {
            return new List<ComponentAll>()
            {
                TextBoxComponent.Set(new ComponentAll()
                {
                    Label = "TexBox",
                    Name = "texbox",
                }),

                TextAreaComponent.Set(new ComponentAll()
                {
                    Label = "TexArea",
                    Name = "txtArea",
                }),

                NumericBoxIntComponent.Set(new ComponentAll()
                {
                    Label = "Int",
                    Name = "numInt",
                }),
                NumericBoxDecimalComponent.Set(new ComponentAll()
                {
                    Label = "Deciaml",
                    Name = "numDecimal",
                }),
                
                SwitchComponent.Set(new ComponentAll()
                {
                    Label = "Switch",
                    Name = "switch",
                }),
            };
        }

        private static List<ComponentValue> GetData()
        {
            var data = new Dictionary<string, object>();
            data.Add("texbox", "");
            data.Add("txtArea", "");
            data.Add("numDecimal", default(decimal));
            data.Add("numInt", default(int));
            data.Add("switch", default(bool));

            var values = new List<ComponentValue>();

            foreach (var item in data)
            {
                values.Add(new ComponentValue { Name = item.Key, Value = item.Value });
            }

            return values;
        }

        /// <summary>
        /// For sending data to update... Represent object of the model from 3th party Key - Value
        /// </summary>
        private readonly List<ComponentValue> EntityData = new();

        private readonly List<ComponentAll> ComponentList;
    }
}

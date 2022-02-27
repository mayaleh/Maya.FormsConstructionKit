using Maya.FormsConstructionKit.Shared.Model.Extention;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityData;
using Maya.FormsConstructionKit.Spa.Library.Extensions;
using Microsoft.JSInterop;

namespace Maya.FormsConstructionKit.Spa.ViewModels.EntityData
{
    public class Actions : IActions
    {
        private readonly IEntityDataViewModel vm;

        public Actions(IEntityDataViewModel vm)
        {
            this.vm = vm;
        }

        public void Edit(object row)
        {
            this.vm.RowManaged = this.vm.EntityData.EntityForm.Components.GetComponentValues(row)
                .ToList();
            this.vm.IsEditingItem = true;
        }

        public void ShowCreateForm()
        {
            this.vm.IsEditingItem = false;
            var keyVals = new Dictionary<string, object>();
            foreach (var item in this.vm.EntityData.EntityForm.Components)
            {
                keyVals.Add(item.Name, item.ValueKind.DefaultValue());
            }
            var strJsonObj = System.Text.Json.JsonSerializer.Serialize(keyVals);
            var objDefault = System.Text.Json.JsonSerializer.Deserialize<object>(strJsonObj);
            this.vm.RowManaged = this.vm.EntityData.EntityForm.Components.GetComponentValues(objDefault!)
                .ToList();
        }

        public async Task Save()
        {
            Console.WriteLine($"Executing save for entity: {this.vm.EntityName}");
            
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(this.vm.RowManaged));

            var keyValObj = this.vm.RowManaged.ToDictionary(keySelector: m => m.Name, elementSelector: x => x.Value);

            var result = this.vm.IsEditingItem ?
                await this.vm.ApiService.ValuesService.UpdateAsync(this.vm.EntityName, keyValObj)
                    .ConfigureAwait(false) :
                await this.vm.ApiService.ValuesService.AddAsync(this.vm.EntityName, keyValObj)
                    .ConfigureAwait(false);

            if (result.IsFailure)
            {
                Console.WriteLine(result.Failure.Message);
                this.vm.NotifyMessage?.Error(result.Failure.Message);
                return;
            }

            this.vm.NotifyMessage?.Success("Saved successfully...");
        }

        public async Task Delete(object row)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(name))
            //{
            //    this.vm.NotifyMessage?.Error("Empty form name.");
            //    return;
            //}

            //var result = await this.vm.ApiService.EntityForm.DeleteAsync(name)
            //    .ConfigureAwait(false);

            //if (result.IsSuccess == false)
            //{
            //    this.vm.NotifyMessage?.Error(result.Failure.Message);
            //    return;
            //}

            //this.vm.NotifyMessage?.Success($"Successfully deleted form: '{name}'");

            //await this.Load()
            //    .ConfigureAwait(false);
        }

        public async Task Load()
        {
            var result = await this.vm.ApiService.ValuesService.GetAllAsync(this.vm.EntityName)
                .ConfigureAwait(false);

            if (result.IsFailure)
            {
                Console.WriteLine(result.Failure.Message);
                this.vm.NotifyMessage?.Error(result.Failure.Message);
            }

            this.vm.EntityData = result.Success ?? Maya.FormsConstructionKit.Shared.Model.EntityData.Create();

            var csvResult = await this.vm.ApiService.CsvDefinitionService.GetAllForEntityAsync(this.vm.EntityName)
                .ConfigureAwait(false);

            if (csvResult.IsFailure)
            {
                Console.WriteLine(csvResult.Failure.Message);
                this.vm.NotifyMessage?.Error(csvResult.Failure.Message);
            }

            this.vm.CsvDefinitions = csvResult.Success;
        }

        public async Task ExportCsv(string csvId)
        {
            var result = await this.vm.ApiService.ValuesService.GetCsvAsync(this.vm.EntityName, csvId)
                .ConfigureAwait(false);

            if (result.IsFailure)
            {
                Console.WriteLine(result.Failure.Message);
                this.vm.NotifyMessage?.Error(result.Failure.Message);
            }

            await this.DownloadFileAsync(result.Success);
        }

        private async Task DownloadFileAsync(FormsConstructionKit.Shared.Model.DownloadFile file)
        {
            await this.vm.JSRuntime.InvokeAsync<object>(
                "FileSaveAsV2",
                file.Content,
                file.ContentType,
                file.Name
            );
        }
    }
}

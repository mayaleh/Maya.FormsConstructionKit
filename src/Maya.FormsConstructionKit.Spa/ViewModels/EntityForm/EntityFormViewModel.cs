using System.Reactive.Linq;
using System.Reactive.Subjects;
using Maya.FormsConstructionKit.Shared.Model;
using Maya.FormsConstructionKit.Spa.Library.Contract.Services;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI.ViewModels.EntityForm;
using Maya.FormsConstructionKit.Spa.Model.UI.View;
using Microsoft.AspNetCore.Components;

namespace Maya.FormsConstructionKit.Spa.ViewModels.EntityForm
{
    public class EntityFormViewModel : BaseViewModel, IEntityFormViewModel, IDisposable
    {
        public INotifyMessage NotifyMessage { get; private init; }

        public IApiService ApiService { get; private init; }
        //public readonly NavigationManager NavigationManager;

        public FormsConstructionKit.Shared.Model.EntityForm EntityForm { get; set; } = new FormsConstructionKit.Shared.Model.EntityForm();

        public ICommands Commands { get; private init; }

        public IActions Actions { get; private init; }

        public ComponentAll ComponentBeforeEdit { get; set; } = null!;

        public ComponentAll ComponentEdit { get; set; } = null!;

        public bool IsComponentEditVisable { get; set; } = false;

        public bool IsCreatingNew { get; set; } = false;

        public bool IsComponentExisting { get; set; }

        private IDisposable IsUnsavedChanges { get; set; }

        private Subject<FormsConstructionKit.Shared.Model.EntityForm> EFSubj { get; set; }

        public EntityFormViewModel(INotifyMessage notifyMessage,
            IApiService apiService,
            Action onUiChanged) : base(onUiChanged)
        {
            OnIsInit = (v) => { OnUiChanged.Invoke(); };
            OnIsBusy = (v) => { OnUiChanged.Invoke(); };
            NotifyMessage = notifyMessage;
            ApiService = apiService;
            //this.NavigationManager = navigationManager;

            Actions = new Actions(this);
            Commands = new Commands(this);

        }

        //private void LoadCommand_OnExecuteChanged(object? sender, bool e) => IsBusy = e;

        //private void InitObservabels()
        //{
        //    IsUnsavedChanges = Observable.Return(EntityForm)
        //        .DistinctUntilChanged()
        //        .Subscribe(async _ =>
        //        {
        //            Console.WriteLine("observable");
        //            await Task.CompletedTask;
        //        });
        //}

        public async Task Init(string? name)
        {
            IsInit = true;

            if (string.IsNullOrEmpty(name))
            {
                IsCreatingNew = true;

                IsInit = false;
                return;
            }

            await this.Load(name)
                .ConfigureAwait(false);

            IsInit = false;
        }

        private async Task Load(string id)
        {
            var result = await ApiService.EntityForm.GetLatestOneAsync(id)
                .ConfigureAwait(false);

            if (result.IsFailure)
            {
                Console.WriteLine(result.Failure.Message);
                NotifyMessage?.Error(result.Failure.Message);
            }

            EntityForm = result.Success;

        }

        public void Dispose()
        {
            IsUnsavedChanges?.Dispose();
            //if (LoadCommand != null)
            //{
            //    LoadCommand.OnExecuteChanged -= this.LoadCommand_OnExecuteChanged;
            //}
        }
    }
}

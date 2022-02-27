namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI
{
    public interface IBaseViewModel
    {
        bool IsInit { get; set; }
        bool IsBusy { get; set; }

        Action<bool>? OnIsInit { get; set; }
        Action<bool>? OnIsBusy { get; set; }
    }
}

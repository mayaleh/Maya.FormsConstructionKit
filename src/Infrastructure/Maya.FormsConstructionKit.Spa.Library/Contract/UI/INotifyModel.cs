using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Maya.FormsConstructionKit.Spa.Library.Contract.UI
{
    public interface INotifyModel : INotifyPropertyChanged
    {
        void SetProperty<T>(ref T backingFiled, T value, [CallerMemberName] string? propertyName = null);
    }
}

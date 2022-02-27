using System.ComponentModel;
using System.Runtime.CompilerServices;
using Maya.FormsConstructionKit.Spa.Library.Contract.UI;

namespace Maya.FormsConstructionKit.Spa.Model.UI.View
{
    public abstract class NotifyModel : INotifyModel
    {
        /// <summary>
        /// Notification on property value changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingFiled"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        public void SetProperty<T>(ref T backingFiled, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;

            backingFiled = value;

            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Notification INotifyPropertyChanged.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

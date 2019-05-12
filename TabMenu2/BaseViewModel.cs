using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TabMenu2
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        internal BaseViewModel()
        {
            //myController = new Controller();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                OnPropertyChanged(propertyName);
            }
        }

        protected bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (field == null || !field.Equals(value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        //public Controller myController;
    }
}

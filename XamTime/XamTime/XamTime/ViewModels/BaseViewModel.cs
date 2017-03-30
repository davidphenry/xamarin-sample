using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XamTime.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel() { }

        protected bool SetPropertyByRef<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;

            OnPropertyChanged(propertyName);

            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //internal static Command InitCommand(Func<Task> innerCommand, Func<bool> canExecute = null)
        //{
        //    if (canExecute == null)
        //        return new Command(async () => await innerCommand.Invoke());
        //    return new Command(async () => await innerCommand.Invoke(), canExecute);
        //}
        //internal static Command InitCommand<T>(Func<T, Task> innerCommand)
        //{
        //    return new Command<T>(async (arg) => await innerCommand.Invoke(arg));
        //}


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        //public virtual void Initialize()
        //{

        //}

        //public async virtual Task NavigatingTo(object parameter, NavigationState navigationState)
        //{

        //}
    }
}

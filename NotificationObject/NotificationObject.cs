using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NotificationObject
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetProperty<T>(ref T target, T value, [CallerMemberName] string caller ="")
        {
            target = value;
            PropertyChangedEventArgs arg = new PropertyChangedEventArgs(caller);
            PropertyChanged?.Invoke(this, arg);
        }
    }
}

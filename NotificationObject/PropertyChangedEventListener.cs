using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NotificationObject
{
    public class PropertyChangedEventListener : IDisposable
    {
        INotifyPropertyChanged Source;
        PropertyChangedEventHandler Handler;

        public PropertyChangedEventListener(INotifyPropertyChanged source, PropertyChangedEventHandler handler)
        {
            Source = source;
            Handler = handler;
            Source.PropertyChanged += Handler;
        }

        public void Dispose()
        {
            if (Source != null && Handler != null)
                Source.PropertyChanged -= Handler;
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationObject
{
    static public class Helper
    {
        public static ReadonlyDispatcherCollection<T> CreateReadonlyDispatcherCollection<T, U>(ObservableCollection<U> source, Func<U, T> converter)
        {
            var target = new ObservableCollection<T>();
            for (int i = 0; i < source.Count; ++ i)
                target.Add(converter(source[i]));

            ReadonlyDispatcherCollection<T> collection = new ReadonlyDispatcherCollection<T>(target);
            source.CollectionChanged += (sender, e) => {
                switch(e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        target.Insert(e.NewStartingIndex, converter((U)e.NewItems[0]));
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        target.Move(e.OldStartingIndex, e.NewStartingIndex);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        target.RemoveAt(e.OldStartingIndex);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        target[e.NewStartingIndex] = converter((U)e.NewItems[0]);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        target.Clear();
                        break;
                }
            };

            return collection;
        }
    }
}
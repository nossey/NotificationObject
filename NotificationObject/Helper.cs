using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationObject
{
    static public class Helper
    {
        public static ReadonlySyncedCollection<T> CreateReadonlySyncedCollection<T, U>(ObservableCollection<U> source, Func<U, T> converter) where T : IDisposable
        {
            var target = new ObservableCollection<T>();

            // Initialization
            for (int i = 0; i < source.Count; ++ i)
                target.Add(converter(source[i]));

            ReadonlySyncedCollection<T> collection = new ReadonlySyncedCollection<T>(target);
            source.CollectionChanged += (sender, e) => {
                switch(e.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        {
                            target.Insert(e.NewStartingIndex, converter((U)e.NewItems[0]));
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        {
                            target.Move(e.OldStartingIndex, e.NewStartingIndex);
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        {
                            target[e.OldStartingIndex].Dispose();
                            target.RemoveAt(e.OldStartingIndex);
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        {
                            target[e.NewStartingIndex].Dispose();
                            target[e.NewStartingIndex] = converter((U)e.NewItems[0]);
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        {
                            foreach (var item in target)
                                item.Dispose();
                            target.Clear();
                        }
                        break;
                }
            };

            return collection;
        }
    }
}
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
    public class ReadonlySyncedCollection<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, IDisposable where T : IDisposable
    {
        #region Declaration
        #endregion

        #region Fields

        #endregion

        #region Properties

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region Public methods

        public ReadonlySyncedCollection() : base(null)
        {
        }

        public ReadonlySyncedCollection(IList<T> list) : base(list)
        {
            if (list == null)
                throw new ArgumentNullException();
        }

        public void Dispose()
        {
            foreach(var item in Items)
            {
                item.Dispose();
            }
        }

        #endregion

        #region Private methods
        #endregion
    }
}
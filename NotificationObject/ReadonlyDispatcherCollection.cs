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
    public class ReadonlyDispatcherCollection<T> : ReadOnlyCollection<T>, IDisposable
    {
        #region Declaration
        #endregion

        #region Fields

        #endregion

        #region Properties

        IList<T> Target;

        #endregion

        #region Public methods

        public ReadonlyDispatcherCollection() : base(null)
        {
        }

        public ReadonlyDispatcherCollection(IList<T> list) : base(list)
        {
            if (list == null)
                throw new ArgumentNullException();
            Target = list;
        }

        public void Dispose()
        {
        }

        #endregion

        #region Private methods
        #endregion
    }
}
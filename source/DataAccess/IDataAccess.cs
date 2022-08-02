using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataAccess
{
    public interface IDataCollection<T>: ICollection<T>
    {
        void Retrieve(Func<T, bool> query = null);
        void Commit();
    }

    public class DataCollection<T> : Collection<T>, IDataCollection<T>
    {
        public void Retrieve(Func<T, bool> query = null)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace DataAccess;

public class JsonFileCollection<T> : Collection<T>, IDataCollection<T>
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
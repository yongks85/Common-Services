using System;

namespace DataAccess
{
    
    public class DataAccessProvider
    {
        public IDataAccess<T> CreateAccess<T>(IAccessType accessType, string connection)
        {
            return accessType.CreateAccess<T>(connection);
        }
        
    }

    public interface IAccessType
    {
        IDataAccess<T> CreateAccess<T>(string connection);
    }

    public  class AccessType
    {
        public static SerializationType File() => new FileAccessType();
    }

    public class FileAccessType : AccessType
    {
       
    }

    public class SerializationType
    {
        //Binary,
        //Json,
        //XML
    }

    public enum DBType
    {
       //Sqlite
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Interfaces.DataStore
{
    public interface IDataStoreService<T> where T : class
    {
        bool AddItem(T item);
        bool UpdateItem(T item, string id);
        bool DeleteItem(string id);
        T GetItem(string id);
        IEnumerable<T> GetAll(bool forceRefresh = false);
    }
}

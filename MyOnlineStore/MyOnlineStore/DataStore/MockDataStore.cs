using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.DataStore
{
    public class MockDataStore<T> : IDataStoreService<T> where T: class
    {
         protected List<T> genericList;

        public MockDataStore()
        {

            genericList = new List<T>();

        }

        public bool AddItem(T item)
        {

            int oldvalue = genericList.Count;

            genericList.Add(item);
            int newvalue = genericList.Count;

            if (oldvalue < newvalue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteItem(string id)
        {


            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(bool forceRefresh = false)
        {
           return genericList;
        }

        public T GetItem(string id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateItem(T item, string id)
        {
            throw new NotImplementedException();
        }
    }
}

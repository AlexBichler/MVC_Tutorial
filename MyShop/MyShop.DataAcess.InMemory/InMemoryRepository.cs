using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAcess.InMemory
{
    public class InMemoryRepository<T> where T : BaseEntity
    {

        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {

            className = typeof(T).Name;

            items = cache[className] as List<T>;

            if (items == null)
            {
                items = new List<T>();
            }

        }

        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {

            T updatedItem = items.Find(i => i.ID == t.ID);

            if (updatedItem != null)
            {
                updatedItem = t;
            }
            else 
            {
                throw new Exception(className + " not found.");
            }

        }

        public T Find(string ID)
        {

            T item = items.Find(i => i.ID == ID);

            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception(className + " not found.");
            }

        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string ID)
        {

            T deletedItem = items.Find(i => i.ID == ID);

            if (deletedItem != null)
            {
                items.Remove(deletedItem);
            }
            else
            {
                throw new Exception(className + " not found.");
            }

        }

    }
}

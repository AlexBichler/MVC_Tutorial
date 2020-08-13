using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAcess.InMemory
{
    public class ProductRepository
    {

        ObjectCache cache = MemoryCache.Default;

        List<Product> products;

        public ProductRepository()
        {

            products = cache["products"] as List<Product>;

            if (products == null)
            {
                products = new List<Product>();
            }

        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product product)
        {
            products.Add(product);
        }

        public void Update(Product product)
        {

            Product updatedProduct = products.Find(p => p.Id == product.Id);

            if (updatedProduct != null)
            {
                updatedProduct = product;
            }
            else 
            {
                throw new Exception("Product not found.");
            }

        }

        public Product Find(string ID)
        {

            Product product = products.Find(p => p.Id == ID);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found.");
            }

        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string ID)
        {
            Product product = products.Find(p => p.Id == ID);

            if (product != null)
            {
                products.Remove(product);
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

    }
}

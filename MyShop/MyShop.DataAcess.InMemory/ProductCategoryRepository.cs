using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAcess.InMemory
{
    public class ProductCategoryRepository
    {

        ObjectCache cache = MemoryCache.Default;

        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {

            productCategories = cache["productCategories"] as List<ProductCategory>;

            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }

        }

        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory category)
        {
            productCategories.Add(category);
        }

        public void Update(ProductCategory category)
        {

            ProductCategory updatedCategory = productCategories.Find(pc => pc.ID == category.ID);

            if (updatedCategory != null)
            {
                updatedCategory = category;
            }
            else
            {
                throw new Exception("Product Category not found.");
            }

        }

        public ProductCategory Find(string ID)
        {

            ProductCategory category = productCategories.Find(pc => pc.ID == ID);

            if (category != null)
            {
                return category;
            }
            else
            {
                throw new Exception("Product Category not found.");
            }

        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string ID)
        {
            ProductCategory categoryToDelete = productCategories.Find(pc => pc.ID == ID);

            if (categoryToDelete != null)
            {
                productCategories.Remove(categoryToDelete);
            }
            else
            {
                throw new Exception("Product Category not found.");
            }
        }

    }
}

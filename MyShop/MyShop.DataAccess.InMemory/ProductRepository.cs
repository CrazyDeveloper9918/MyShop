using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
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
            
        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product product)
        {
            Product ProductToUpdate = products.Find(m => m.ID == product.ID);

            if (ProductToUpdate!=null)
            {
                ProductToUpdate = product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }

        public Product Find(string ID)
        {
            Product Product = products.Find(m => m.ID == ID);

            if (Product != null)
            {
                 return Product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }

        public IQueryable<Product> List()
        {
            return products.AsQueryable();
        }

        public void Delete(string ID)
        {
            Product ProductTODelete = products.Find(m => m.ID == ID);

            if (ProductTODelete != null)
            {
                products.Remove(ProductTODelete);
            }
            else
            {
                throw new Exception("Product no found");
            }
        }
    }
}

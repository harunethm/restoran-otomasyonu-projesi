using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        IProduct _product;

        public ProductManager(IProduct product)
        {
            _product = product;
        }

        public List<Product> ListAll()
        {
            return _product.List();
        }

        public void AddProduct(Product p)
        {
            _product.Insert(p);
        }

        public Product GetByID(int p)
        {
            return _product.Get(x => x.ProductID == p);
        }

        public List<Product> GetByCategoryID(int p)
        {
            return _product.List(x => x.CategoryID == p);
        }

        public int IsProductExist(string productName)
        {
            Product _ = _product.Get(x => x.Name == productName);
            return _ != null ? _.ProductID : 0;
        }

        public void UpdateProduct(Product p)
        {
            _product.Update(p);
        }
    }
}

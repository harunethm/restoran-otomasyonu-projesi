using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProductService
    {
        void AddProduct(Product p);
        List<Product> ListAll();
        Product GetByID(int p);
        List<Product> GetByCategoryID(int p);
    }
}

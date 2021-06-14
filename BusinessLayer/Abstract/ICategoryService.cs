using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        List<Category> ListAll();
        void AddCategory(Category p);
        Category GetByID(int p);
        List<Category> GetForMenu();
        void Delete(Category p);
        int IsCategoryExist(string categoryName);
        void UpdateCategory(Category p);
    }
}

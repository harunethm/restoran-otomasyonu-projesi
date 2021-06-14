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
    public class CategoryManager : ICategoryService
    {
        ICategory _category;

        public CategoryManager(ICategory category)
        {
            _category = category;
        }

        public List<Category> ListAll()
        {
            return _category.List();
        }

        public void AddCategory(Category p)
        {
            _category.Insert(p);
        }

        public Category GetByID(int id)
        {
            return _category.Get(x => x.CategoryID == id);
        }

        public int IsCategoryExist(string categoryName)
        {
            Category _ = _category.Get(x => x.CategoryName == categoryName);
            return _ != null ? _.CategoryID : 0;
        }

        public void Delete(Category p)
        {
            p.Status = false;
            _category.Update(p);
        }

        public void UpdateCategory(Category p)
        {
            _category.Update(p);
        }

        public List<Category> GetForMenu()
        {
            return _category.List(x => x.Status && x.Products.Count(y => y.Status) > 0);
        }
    }
}

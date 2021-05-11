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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CategoryManager
    {
        GenericRepository<Category> repo = new GenericRepository<Category>();

        public List<Category> ListAll()
        {
            return repo.List();
        }

        public void AddCategory(Category p)
        {
            if (/*p geçerli değil ise*/false)
            {
                //hata mesajı
            }
            else // p geçerli ise
            {
                repo.Insert(p);
            }
        }

    }
}

using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        List<User> ListAll();
        void AddUser(User p);
        User GetByID(int p);
        User GetByPhoneNumber(string p);
        void DeleteUser(User p);
        void UpdateUser(User p);

    }
}

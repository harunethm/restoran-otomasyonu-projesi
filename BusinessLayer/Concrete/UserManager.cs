using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUser _user;

        public UserManager(IUser user)
        {
            _user = user;
        }

        public void AddUser(User user)
        {
            _user.Insert(user);
        }

        public void DeleteUser(User p)
        {
            p.Status = false;
            _user.Update(p);
        }

        public int GetAdminCount()
        {
            return _user.List().Count(x => x.Role == "admin" && x.Status == true);
        }

        public User GetByID(int id)
        {
            return _user.Get(x => x.UserID == id);
        }

        public User GetByPhoneNumber(string p)
        {
            return _user.Get(x => x.PhoneNumber == p);
        }

        public List<User> ListAll()
        {
            return _user.List();
        }

        public void UpdateUser(User p)
        {
            _user.Update(p);
        }
    }
}

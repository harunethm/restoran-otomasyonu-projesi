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

        public User GetByID(int id)
        {
            return _user.Get(x => x.UserID == id);
        }

        public List<User> ListAll()
        {
            return _user.List();
        }
    }
}

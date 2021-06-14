using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderService
    {
        void AddOrder(Order p);
        List<Order> ListAll();
        Order GetByID(int p);
        List<Order> GetByReceiptID(int p);
        void Update(Order p);

    }
}

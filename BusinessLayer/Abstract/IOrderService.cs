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
        void Update(Order p);
        Order GetByID(int p);
        List<Order> ListAll();
        List<Order> GetByReceiptID(int p);
        List<Order> GetForOrdersPage();
        List<Order> GetForOrdersPage(int p);
    }
}

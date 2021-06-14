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
    public class OrderManager : IOrderService
    {
        IOrder _order;

        public OrderManager(IOrder order)
        {
            _order = order;
        }

        public List<Order> ListAll()
        {
            return _order.List();
        }

        public void AddOrder(Order p)
        {
            _order.Insert(p);
        }

        public Order GetByID(int p)
        {
            return _order.Get(x => x.OrderID == p);
        }

        public List<Order> GetByReceiptID(int p)
        {
            return _order.List(x => x.ReceiptID == p);
        }

        public List<Order> GetInCartByReceiptID(int p)
        {
            return _order.List(x => x.ReceiptID == p && x.Status == 0);
        }

        public void Update(Order p)
        {
            _order.Update(p);
        }
    }
}

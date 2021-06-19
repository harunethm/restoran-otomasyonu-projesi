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
    public class PaymentManager : IPaymentService
    {
        IPayment _payment;

        public PaymentManager(IPayment payment)
        {
            _payment = payment;
        }

        public void Add(Payment p)
        {
            _payment.Insert(p);
        }

        public Payment GetByID(int p)
        {
            return _payment.Get(x => x.PaymentID == p);
        }

        public List<Payment> GetByOrderID(int p)
        {
            return _payment.List(x => x.OrderID == p);
        }

        public List<Payment> ListAll()
        {
            return _payment.List();
        }

        public void Update(Payment p)
        {
            _payment.Update(p);
        }
    }
}

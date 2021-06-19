using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPaymentService
    {
        void Add(Payment p);
        void Update(Payment p);
        List<Payment> ListAll();
        Payment GetByID(int p);
        List<Payment> GetByOrderID(int p);
    }
}

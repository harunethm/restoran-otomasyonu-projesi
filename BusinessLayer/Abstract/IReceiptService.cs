using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IReceiptService
    {
        List<Receipt> ListAll();
        void AddReceipt(Receipt p);
        Receipt GetByID(int id);
        Receipt GetLast();
        void Update(Receipt p);
    }
}

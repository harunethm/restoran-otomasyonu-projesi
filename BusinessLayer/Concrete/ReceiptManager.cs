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
    public class ReceiptManager : IReceiptService
    {
        IReceipt _receipt;

        public ReceiptManager(IReceipt receipt)
        {
            _receipt = receipt;
        }

        public List<Receipt> ListAll()
        {
            return _receipt.List();
        }

        public void AddReceipt(Receipt p)
        {
            _receipt.Insert(p);
        }

        public Receipt GetByID(int id)
        {
            return _receipt.Get(x => x.ReceiptID == id);
        }

        public Receipt GetLast()
        {
            return _receipt.List().LastOrDefault();
        }

        public void Update(Receipt p)
        {
            _receipt.Update(p);
        }
    }
}

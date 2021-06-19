using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITakeAwayService
    {
        List<TakeAway> ListAll();
        void AddTakeAway(TakeAway p);
        TakeAway GetByReceiptID(int p);
        void Update(TakeAway p);
        List<TakeAway> GetForOrdersPage();
    }
}

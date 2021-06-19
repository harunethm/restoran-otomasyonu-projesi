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
    public class TakeAwayManager : ITakeAwayService
    {
        ITakeAway _takeAway;

        public TakeAwayManager(ITakeAway takeAway)
        {
            _takeAway = takeAway;
        }

        public List<TakeAway> ListAll()
        {
            return _takeAway.List();
        }

        public void AddTakeAway(TakeAway p)
        {
            _takeAway.Insert(p);
        }

        public TakeAway GetByReceiptID(int p)
        {
            return _takeAway.Get(x => x.ReceiptID == p);
        }

        public void Update(TakeAway p)
        {
            _takeAway.Update(p);
        }

        public List<TakeAway> GetForOrdersPage()
        {
            return _takeAway.List(x => x.Receipt.Orders.Count(y => y.Status == 1) > 0);
        }
    }
}

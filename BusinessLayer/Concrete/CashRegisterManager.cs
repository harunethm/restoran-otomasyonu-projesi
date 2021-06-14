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
    public class CashRegisterManager : ICashRegisterService
    {
        ICashRegister _cashRegister;

        public CashRegisterManager(ICashRegister cashRegister)
        {
            _cashRegister = cashRegister;
        }

        public void AddCashRegister(CashRegister p)
        {
            _cashRegister.Insert(p);
        }

        public void DayStatusChange()
        {
            CashRegister cashRegister = _cashRegister.Get(x => x.Status == true);
            if (cashRegister != null)
            {
                cashRegister.Status = false;
                _cashRegister.Update(cashRegister);
            }
        }

        public CashRegister GetByID(int p)
        {
            return _cashRegister.Get(x => x.CashRegisterID == p);
        }

        public CashRegister IsDayStarted()
        {
            return _cashRegister.Get(x => x.Status == true);
        }

        public List<CashRegister> ListAll()
        {
            return _cashRegister.List().OrderByDescending(x => x.DayStart.Day).ThenByDescending(x => x.DayStart.Hour).ThenByDescending(x => x.DayStart.Minute).ToList();
        }

        public void Update(CashRegister p)
        {
            _cashRegister.Update(p);
        }
    }
}

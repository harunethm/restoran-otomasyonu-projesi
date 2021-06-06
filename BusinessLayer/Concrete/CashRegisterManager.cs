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

        public CashRegister GetByID(int p)
        {
            return _cashRegister.Get(x => x.CashRegisterID == p);
        }

        public List<CashRegister> ListAll()
        {
            return _cashRegister.List();
        }
    }
}

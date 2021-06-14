using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICashRegisterService
    {
        List<CashRegister> ListAll();
        void AddCashRegister(CashRegister p);
        CashRegister GetByID(int p);
        void DayStatusChange();
        void Update(CashRegister p);
        CashRegister IsDayStarted();
    }
}

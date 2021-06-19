using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITableService
    {
        List<Table> ListAll();
        List<Table> GetForOrdersPage();
        void AddTable(Table p);
        Table GetByID(int p);
        void Update(Table p);
        bool IsAllEmpty();
        Table GetByReceiptID(int p);

    }
}

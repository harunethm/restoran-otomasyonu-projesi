using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TableManager : ITableService
    {
        ITable _table;

        public TableManager(ITable table)
        {
            _table = table;
        }

        public List<Table> ListAll()
        {
            return _table.List();
        }

        public void AddTable(Table p)
        {
            _table.Insert(p);
        }

        public Table GetByID(int p)
        {
            return _table.Get(x => x.TableID == p);
        }

        public void Update(Table p)
        {
            _table.Update(p);
        }

        public List<Table> GetTablesForOrders()
        {
            return _table.List(x => x.Status && x.Receipt.Orders.Where(y => y.Status == 1 || y.Status == 2).ToList().Count > 0);
        }

        public bool IsAllEmpty()
        {
            return _table.List(x => x.Availability > 0 && x.Status == true).Count > 0 ? false : true;
        }
    }
}

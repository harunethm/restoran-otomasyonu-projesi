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
    }
}

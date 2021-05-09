using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Table
    {
        [Key]
        public int TableID { get; set; }

        // Masanın durumu; 0 => Boş, 1 => Aktif, 2 => Rezerve edilmiş
        public int Status { get; set; }

        // Masa eğer aktif ise aktif olma tarihi
        public DateTime? OpeningDate { get; set; }

        // Siparişler
        public ICollection<Receipt> Receipts { get; set; }
    }
}

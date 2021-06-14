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

        // Masanın takma adı => Bahçe-1, İçeri Sağ-1, İçeri Sol-3 vs.
        public string Name { get; set; }

        // Masanın durumu; 0 => Boş, 1 => Aktif, 2 => Rezerve edilmiş
        public int Availability { get; set; }

        // Masa eğer aktif ise aktif olma tarihi
        public DateTime? OpeningDate { get; set; }

        // Masanın kullanımda olup olmama durumu
        public bool Status { get; set; }

        // Siparişler
        public int? ReceiptID { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}

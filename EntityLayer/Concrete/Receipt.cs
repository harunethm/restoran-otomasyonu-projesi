using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Receipt
    {
        [Key]
        public int ReceiptID { get; set; }

        // Verilen siparişlerin toplam tutarı
        public double Total { get; set; }

        // Yapılan indirim
        public double Discount { get; set; }

        // Ödeme türü; ödenmemiş ise => 0, nakit ödenmiş ise => 1, kredi kartı ile ödenmiş ise => 2
        public int PaymentMethod { get; set; }

        // Fatura oluşturulma tarihi ve zamanı
        public DateTime ReceiptDate { get; set; }

        // Fatura güncel mi eski mi (ödenmiş mi ödenmemiş mi)
        public bool Status { get; set; }

        public ICollection<Table> Tables { get; set; }

        public ICollection<TakeAway> TakeAways { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}

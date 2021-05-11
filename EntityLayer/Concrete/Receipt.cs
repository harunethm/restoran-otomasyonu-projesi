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

        // Ödeme türü; false => nakit, true => kredi kartı
        public bool PaymentMethod { get; set; }

        // Faturanın ait olduğu masanın ID'si, eğer paket servis ise 0(sıfır)
        public int TableID { get; set; }
        public virtual Table Table { get; set; }

        // Eğer paket servis ise ID, değil ise 0(sıfır)
        public int TakeAwayID { get; set; }
        public virtual TakeAway TakeAway { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}

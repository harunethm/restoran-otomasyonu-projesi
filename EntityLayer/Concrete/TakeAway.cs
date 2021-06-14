using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
   public class TakeAway
    {
        [Key]
        public int TakeAwayID { get; set; }

        [StringLength(100)] // Sipariş veren müşterinin adresi
        public string CustomerAdress { get; set; }

        [StringLength(20)] // Müşterinin adı
        public string CustomerName { get; set; }

        [StringLength(10)] // Müşterinin iletişim numarası
        public string CustomerPhoneNumber { get; set; }

        // Siparişin durumu; 0 => sipariş alınmış ancak daha yola çıkmamış, 1 => gönderilmiş ancak daha ödenmemiş, 2 => gönderilmiş ve ödenmiş
        public int Status { get; set; }

        // Siparişler
        public int ReceiptID { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}

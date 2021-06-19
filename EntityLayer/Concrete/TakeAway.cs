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

        // sipariş alındı => 0, sipariş hazır => 1, sipariş ödendi => 2
        public int Status { get; set; }

        // Siparişler
        public int ReceiptID { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}

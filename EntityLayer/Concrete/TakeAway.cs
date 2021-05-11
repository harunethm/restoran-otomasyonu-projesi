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

        // Müşterinin ödeme yöntemi 0 => karışık, 1 => nakit, 2 => kredi kartı 
        public int PaymentMethod { get; set; }

        // Siparişler
        ICollection<Receipt> Receipts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        // ödeme tarihi
        public DateTime PaymentDateTime { get; set; }

        //hangi sipariş
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }

        // kaç adedi ödeniyor
        public int Amount { get; set; }

        // Ödeme türü; nakit => 0, kredi kartı => 1, ikram => 2
        public int PaymentMethod { get; set; }

        // bu ürünler için toplam ödenmesi gereken
        public double Total { get; set; }

    }
}

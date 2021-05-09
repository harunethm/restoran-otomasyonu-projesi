﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        // Sipariş verilen ürünün adedi
        public int Amount { get; set; }

        [StringLength(100)] // Sipariş verilen ürün için açıklama
        public string Description { get; set; }

        // Siparişin; sepette mi, onaylanmış mı yoksa geçmiş sipariş mi olduğu bilgisi
        public int Status { get; set; }

        // Siparişin verildiği tarih ve saat
        public DateTime OrderDate { get; set; }

        // Sipariş verilen ürünün ID'si
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        // Siparişi alınan masanın ID'si
        public int ReceiptID { get; set; }
        public virtual Receipt Receipt { get; set; }

        // Siparişleri alan garsonun ID'si
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}

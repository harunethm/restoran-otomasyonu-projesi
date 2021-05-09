﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CashRegister
    {
        [Key]
        public int CashRegisterID { get; set; }

        // Bu günün tarihi
        public DateTime DateTime { get; set; }

        // Gün başlangıcı yapılıp yapılmadığı
        public bool Status { get; set; }

        // Günün kazancı
        public double AmountEarned { get; set; }

        // Gün boyu ikram edilenlerin tutarı
        public double AmountServed { get; set; }

        // Gün boyu yapılan indirim tutarı
        public double AmountDiscount { get; set; }
    }
}

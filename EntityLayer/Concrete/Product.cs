using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [StringLength(50)] // Ürün ismi
        public string Name { get; set; }

        // Ürünün birim fiyatı
        public double Price { get; set; }

        [StringLength(100)] // Ürün açıklaması
        public string Description { get; set; }

        // Ürünün satışta olup olmama durumu
        public bool Status { get; set; }

        // Ürünün ait olduğu kategorinin ID'si
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}

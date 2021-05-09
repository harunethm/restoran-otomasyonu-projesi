using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(50)] // Kategori adı
        public string CategoryName { get; set; }

        // Kategorinin aktif olup olmadığı
        public bool Status { get; set; }

        // Kategorideki ürünler
        public ICollection<Product> Products { get; set; }
    }
}

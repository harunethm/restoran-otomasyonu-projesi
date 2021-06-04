using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [StringLength(20)] // Kişinin adı soyadı
        public string Name { get; set; }

        [StringLength(20)] // Kişinin adı soyadı
        public string Surname { get; set; }

        [StringLength(50)] // Otomasyona giriş şifresi
        public string Password { get; set; }

        [StringLength(10)] // İletişim numarası
        public string PhoneNumber { get; set; }

        // Yetki düzeyi; 0 => Aşçı, 1 => Garson, 2 => Admin 
        public int AuthorityLevel { get; set; }

        // Sisteme kayıt olma tarihi
        public DateTime RegisterDate { get; set; }

        // Sisteme son giriş yaptığı tarih
        public DateTime LastLogin { get; set; }

        // Son şifre değiştirme tarihi
        public DateTime LastPasswordChange { get; set; }

        // Kullanıcı hala çalışıyor mu; 1 => aktif, 0 => pasif
        public bool Status { get; set; }

        // Aldığı siparişler
        public ICollection<Order> Orders { get; set; }
    }
}

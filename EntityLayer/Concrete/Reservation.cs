using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [StringLength(50)] // Rezerve eden kişinin ismi
        public string Name { get; set; }

        [StringLength(10)] // Rezerve eden kişiye ait telefon numarası
        public string PhoneNumber { get; set; }

        // Rezervasyon işleminin yapıldığı tarih
        public DateTime ReservationTaken { get; set; }

        // Rezerve edildiği tarih
        public DateTime ReservationFor { get; set; }

        // Rezerve edilen masanın ID'si
        public int TableID { get; set; }
        public virtual Table Table { get; set; }
    }
}

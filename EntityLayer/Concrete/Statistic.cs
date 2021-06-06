using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Statistic
    {
        [Key]
        public int ID { get; set; }

        public string StatisticHeader { get; set; }

        public string StatisticValue { get; set; }
    }
}

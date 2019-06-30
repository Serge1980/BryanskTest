using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GizMeteoWCF.Db
{
    class City_Weather
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Cityes")]
        public int CityId { get; set; }
        public string Temp { get; set; }
        public DateTime Date { get; set; }
       
        public Cityes Cityes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFAF52_HFT_2022232.Models
{
    //{Id = 1, CompanyName = "Mon Calamari Shipyards", Faction = "Rebel Alliance" }
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(30)]
        public string CompanyName { get; set; }
        public string Faction { get; set; }
    }
}

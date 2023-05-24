using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TFAF52_HFT_2022232.Models
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Faction { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Ship> Ships { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Planet> Planets { get; set; }

        public Company()
        {
            Ships = new HashSet<Ship>();
            Planets = new HashSet<Planet>();
        }
    }
}

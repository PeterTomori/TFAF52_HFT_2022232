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
    public class Ship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShipId { get; set; }
        public string ShipName { get; set; }
        public string ShipType { get; set; }
        public int CompanyId { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }

        public Ship()
        {

        }

        public Ship(int id, string name, string type, int companyId)
        {
            ShipId = id;
            ShipName = name;
            ShipType = type;
            CompanyId = companyId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFAF52_HFT_2022232.Models
{
    //Id = 1, ShipName = "Mon Calamari Cruiser", ShipType = "Capital Ship", CompanyId = 1}
    public class Ship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string ShipName { get; set; }
        public string ShipType { get; set; }
        public int CompanyId { get; set; }
    }
}

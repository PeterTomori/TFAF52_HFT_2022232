using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TFAF52_HFT_2022232.Models
{

    //{Id = 1, PlanetName = "Mon Cala" , Company = "Mon Calamari Shipyards" },
    public class Planet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanetId { get; set; }
        public string PlanetName { get; set; }
        public int CompanyId { get; set; }

        [JsonIgnore]
        public virtual Company Company { get; set; }

        public Planet()
        {

        }

        public Planet(int id, string name, int companyId)
        {
            PlanetId = id;
            PlanetName = name;
            CompanyId = companyId;
        }
    }
}

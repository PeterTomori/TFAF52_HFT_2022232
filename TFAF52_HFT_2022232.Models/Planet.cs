﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFAF52_HFT_2022232.Models
{

    //{Id = 1, PlanetName = "Mon Cala" , Company = "Mon Calamari Shipyards" },
    public class Planet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(20)]
        public string PlanetName { get; set; }
        public int CompanyId { get; set; }
    }
}
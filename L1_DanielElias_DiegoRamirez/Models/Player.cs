using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace L1_DanielElias_DiegoRamirez.Models
{
    public class Player
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        public string Club { get; set; }


    }
}

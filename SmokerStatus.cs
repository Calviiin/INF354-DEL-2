using System;
using System.ComponentModel.DataAnnotations;

namespace OurApi.Models
{
    public class SmokerStat
    {
        [Key]
        public int SmokId { get; set; }
        public bool Smokstat { get; set; }

    }
}

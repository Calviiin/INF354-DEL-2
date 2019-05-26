using System;
using System.ComponentModel.DataAnnotations;

namespace OurApi.Models
{
    public class Treatments
    {
        //Treatment Identifiers
        [Key]
        public int TreatId { get; set; }

        public string TreatName { get; set; }

        public string TreatDescption { get; set; }

    }
}

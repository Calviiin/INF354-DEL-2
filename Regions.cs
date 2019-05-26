using System;
using System.ComponentModel.DataAnnotations;

namespace OurApi.Models
{
    public class Regions
    {
        [Key]
        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
}

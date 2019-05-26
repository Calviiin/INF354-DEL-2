using System;
using System.ComponentModel.DataAnnotations;

namespace OurApi.Models
{
    public class Symptoms
    {
        [Key]
        public int SympId { get; set; }

        public string SympName { get; set; }

        public string SympDescption { get; set; }
    }
}

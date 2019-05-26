using System;
using System.ComponentModel.DataAnnotations;

namespace OurApi.Models
{
    public class HelpLines
    {
        [Key]
        public int HelpId { get; set; }

        public string HelpName { get; set; }

        public string HelpNumber { get; set; }
    }
}

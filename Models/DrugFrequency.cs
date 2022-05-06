using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class DrugFrequency
    {
        public int DrugFrequencyId { get; set; }
        [Required]
        public string FrequencyName { get; set; }
        public int DrugId { get; set; }
        public Drug? Drug { get; set; }
    }
}

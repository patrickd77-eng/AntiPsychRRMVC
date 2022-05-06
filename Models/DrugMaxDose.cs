using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class DrugMaxDose
    {
        public int DrugMaxDoseID { get; set; }
        [Required]
        public decimal MaximumDoseLimit { get; set; }

        public int DrugId { get; set; }
        public Drug? Drug { get; set; }
    }
}

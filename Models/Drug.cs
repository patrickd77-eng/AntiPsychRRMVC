using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC.Models
{
    public class Drug
    {
        [Key]
        public int DrugId { get; set; }
        [Required]
        [DisplayName("Drug Name")]
        public string DrugName { get; set; }
        [DisplayName("Frequency")]
        public DrugFrequency DrugFrequency { get; set; }
        [DisplayName("Route")]
        public DrugRoute DrugRoute { get; set; }
        [DisplayName("Max Dose")]
        public DrugMaxDose DrugMaxDose { get; set; }
    }
}
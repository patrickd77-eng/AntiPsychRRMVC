using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class Drug
    {
        [Key]
        public int DrugId { get; set; }
        [Required]
        [DisplayName("Drug Name")]
        public string DrugName { get; set; }
        [DisplayName("Drug Frequency")]
        public List<DrugFrequency> DrugFrequency { get; set; }
        [DisplayName("Drug Max Dose")]
        public List<DrugMaxDose> DrugMaxDose { get; set; }
        [DisplayName("Drug Route")]
        public List<DrugRoute> DrugRoute { get; set; }
    }
}
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
        public ICollection<DrugFrequency> DrugFrequency { get; set; }
        [DisplayName("Drug Max Dose")]
        public ICollection<DrugMaxDose> DrugMaxDose { get; set; }
        [DisplayName("Drug Route")]
        public ICollection<DrugRoute> DrugRoute { get; set; }
    }
}
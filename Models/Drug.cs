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
        public ICollection<DrugFrequency> DrugFrequency { get; set; }
        public ICollection<DrugMaxDose> DrugMaxDose { get; set; }
        public ICollection<DrugRoute> DrugRoute { get; set; }
         


    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class DrugMaxDose
    {
        [Key]
        public int DrugMaxDoseID { get; set; }
        [Required(ErrorMessage = "Please enter a dose limit for this drug.")]
        [Column(TypeName = "decimal(6,2)")]
        [DisplayName("Max Dose")]
        public decimal MaximumDoseLimit { get; set; }
        [ForeignKey("DrugId")]
        public int DrugId { get; set; }
    }
}
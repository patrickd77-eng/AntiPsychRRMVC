using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class DrugMaxDose
    {
        [Key]
        public int DrugMaxDoseID { get; set; }
        [Required]
        [Column(TypeName = "decimal(6,2)")]
        [DisplayName("Max Dose")]
        public decimal MaximumDoseLimit { get; set; }

        public Drug Drug { get; set; }
    }
}

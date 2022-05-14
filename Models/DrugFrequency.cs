using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class DrugFrequency
    {
        [Key]
        public int FrequencyId { get; set; }
        [Required(ErrorMessage = "Please enter a frequency for this drug. EG Once daily.")]
        [DisplayName("Frequency Details")]
        [DataType(DataType.Text)]
        public string FrequencyDetails { get; set; }
        [ForeignKey("DrugId")]
        public int DrugId { get; set; }
    }
}
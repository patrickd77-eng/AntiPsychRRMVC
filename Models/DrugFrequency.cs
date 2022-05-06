using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class DrugFrequency
    {
        [Key]
        public int FrequencyId { get; set; }
        [Required]
        [DisplayName("Frequency Name")]
        public string FrequencyDetails { get; set; }
        public Drug Drug { get; set; }

    }
}

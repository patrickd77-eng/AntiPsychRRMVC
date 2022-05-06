using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class Drug
    {

        public int DrugId { get; set; }
        [Required]
        public string DrugName { get; set; }

        public int DrugMaxDoseID { get; set; }
        public int DrugRouteID { get; set; }
        public int DrugFrequencyID { get; set; }


    }
}

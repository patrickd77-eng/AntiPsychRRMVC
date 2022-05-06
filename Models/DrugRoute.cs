using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class DrugRoute
    {
        public int DrugRouteId { get; set; }
        [Required]
        public string RouteName { get; set; }
        public int DrugId { get; set; }
        public Drug? Drug { get; set; }
    }
}

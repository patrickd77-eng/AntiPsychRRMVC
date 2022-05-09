using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC2.Models
{
    public class DrugRoute
    {
        [Key]
        public int RouteId { get; set; }
        [Required]
        [DisplayName("Route Name")]
        public string RouteName { get; set; }
        public Drug Drug { get; set; }
    }
}
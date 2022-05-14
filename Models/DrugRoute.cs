using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntiPsychRRMVC.Models
{
    public class DrugRoute
    {
        [Key]
        public int RouteId { get; set; }
        [Required(ErrorMessage = "Please enter a administration route for this drug, eg Oral.")]
        [DisplayName("Route Name")]
        [DataType(DataType.Text)]
        public string RouteName { get; set; }
        [ForeignKey("DrugId")]
        public int DrugId { get; set; }
       
    }
}
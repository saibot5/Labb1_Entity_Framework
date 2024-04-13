using Labb1_Entity_Framework.Areas.Identity.Data;
using Labb1_Entity_Framework.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1_Entity_Framework.Models
{
    public class VacationRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public VacationTypes VacationType { get; set; }
        public DateTime CreatedDate { get; set; }
        public Labb1User? Employee { get; set; }
        public bool accepted { get; set; }
    }
}

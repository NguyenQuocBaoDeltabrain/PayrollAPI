using System.ComponentModel.DataAnnotations.Schema;
namespace SuperHeroAPI.Models
{
    public class OverTime
    {
        public int Id { get; set; }
        public DateTime StartAt {get; set; }
        public DateTime EndAt { get; set; }

        public bool IsSalary { get; set; }

    
        public int? StaffId { get; set; } = null;

   
        public Staff? Staff { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PayrollAPI.Models
{
    public class Staff
    {
        public int id { get; set; }
        [MaxLength(50)]
        public string name { get; set; }
        public float salary { get; set; }
        public DateTime dateOfBirth { get; set; }
        [MaxLength(10)]
        public string sex { get; set; }

        public bool isActive { get; set; } = true;
        [ForeignKey("staffId")]
        public List<OverTime> overTimes { get; set; }
        [ForeignKey("staffId")]
        public List<Salary> salaries { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
namespace SuperHeroAPI.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public float Salary { get; set; }
        public float Age { get; set; }
        public string Sex { get; set; }

        [ForeignKey("StaffId")]
        public List<OverTime>? Overtimes { get; set; }

        [InverseProperty("Staff")]
        public List<Salary>? Salaries { get; set; }
    }
}

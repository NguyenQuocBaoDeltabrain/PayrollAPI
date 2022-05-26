using System.ComponentModel.DataAnnotations;
namespace SuperHeroAPI.Validations
{
    public class StaffRequest
    {
        [Required] public string Name { get; set; }

        [Required] public float Salary { get; set; }
        [Required] public float Age { get; set; }
        [Required] public string Sex { get; set; }
    }
}

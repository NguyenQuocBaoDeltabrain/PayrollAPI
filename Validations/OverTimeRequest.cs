using System.ComponentModel.DataAnnotations;
namespace SuperHeroAPI.Validations
{
    public class OverTimeRequest
    {
   
        [Required] public DateTime StartAt { get; set; }
        [Required] public DateTime EndAt { get; set; }

        [Required]  public bool IsSalary { get; set; }
    }
}

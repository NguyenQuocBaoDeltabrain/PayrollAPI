using System.ComponentModel.DataAnnotations;
namespace SuperHeroAPI.Validations
{
    public class SalaryRequest
    {
        [Required] public int Id { get; set; }
        [Required] public string Month { get; set; }
    }
}

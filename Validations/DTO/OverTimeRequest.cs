using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Validations.DTO
{
    public class OverTimeRequest
    {
        [Required] public DateTime startAt { get; set; }
        [Required] public DateTime endAt { get; set; }
        [Required] public bool isSalary { get; set; }
        [Required] public int staffId { get; set; }
    }
}
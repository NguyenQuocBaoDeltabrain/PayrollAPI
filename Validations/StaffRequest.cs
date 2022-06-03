using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Validations
{
    public class StaffRequest
    {
        [MaxLength(50)]
        [Required] public string name { get; set; }
        [Required] public float salary { get; set; }
        [Required] public DateTime dateOfBirth { get; set; }
        [MaxLength(10)]
        [Required] public string sex { get; set; }
    }
}

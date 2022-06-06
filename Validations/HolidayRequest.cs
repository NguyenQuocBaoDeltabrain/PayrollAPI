using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Validations
{
    public class HolidayRequest
    {
        [MaxLength(10)]
        [Required] public string feteday { get; set; }
    }
}

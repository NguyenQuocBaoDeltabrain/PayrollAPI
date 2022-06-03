using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Validations
{
    public class SalaryRequest
    {
        [MaxLength(10)]
        [Required] public string month { get; set; }
    }
}
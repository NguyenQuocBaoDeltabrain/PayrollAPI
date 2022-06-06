using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Validations.DTO
{
    public class SalaryRequest
    {
        [MaxLength(10)]
        [Required] public string month { get; set; }
    }
}
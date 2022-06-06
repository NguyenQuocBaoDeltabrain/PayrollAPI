using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Validations.DTO
{
    public class FindStaffAndOTRequest
    {
        [MaxLength(10)]
        [Required] public string month { get; set; }
    }
}

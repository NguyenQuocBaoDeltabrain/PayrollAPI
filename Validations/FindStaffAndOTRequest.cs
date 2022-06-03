using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Validations
{
    public class FindStaffAndOTRequest
    {
        [Required] public int staffId { get; set; }
        [MaxLength(10)]
        [Required] public string month { get; set; }
    }
}

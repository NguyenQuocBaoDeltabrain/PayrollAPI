using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Models
{
    public class Holiday
    {
        public int id { get; set; }
        [MaxLength(10)]
        public string feteday { get; set; } 
    }
}
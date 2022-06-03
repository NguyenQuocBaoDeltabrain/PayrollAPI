using System.ComponentModel.DataAnnotations;
namespace PayrollAPI.Models
{
    public class Salary
    {
        public int id { get; set; }
        [MaxLength(10)]
        public string month { get; set; }
        public float salaryBasic { get; set; }
        public float salaryOT { get; set; }
        public float tax { get; set; }
        public float insurance { get; set; }
        public float salaryReceived { get; set; }
        public bool isDelivered { get; set; }
        public int staffId { get; set; }
        public Staff staff { get; set; }
    }
}
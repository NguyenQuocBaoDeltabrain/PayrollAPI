namespace PayrollAPI.Models
{
    public class OverTime
    {
        public int id { get; set; }
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }
        public bool isSalaryCalculated { get; set; }
        public int staffId { get; set; }
        public Staff staff { get; set; }
    }
}
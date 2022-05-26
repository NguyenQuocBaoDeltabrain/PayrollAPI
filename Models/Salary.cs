namespace SuperHeroAPI.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public string Month { get; set; }

        public float SalaryBasic { get; set; }
        public float SalaryOT { get; set; }
        public float Tax { get; set; }
        public float Insurance { get; set; }

        public float SalaryReceived { get; set; }

        public bool IsDelivered { get; set; }

        public Staff? Staff { get; set; }
    }
}

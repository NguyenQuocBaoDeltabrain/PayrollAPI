namespace PayrollAPI.Validations.RO
{
    public class SalaryResponse
    {
        public string month { get; set; }
        public float salaryBasic { get; set; }
        public float salaryOT { get; set; }
        public float tax { get; set; }
        public float insurance { get; set; }
        public float salaryReceived { get; set; }
    }
}
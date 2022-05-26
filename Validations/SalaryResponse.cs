namespace SuperHeroAPI.Validations
{
    public class SalaryResponse
    {
        public int Id { get; set; }
        public string Month { get; set; }

        public float SalaryBasic { get; set; }
        public float SalaryOT { get; set; }
     
        public float SalaryReceived { get; set; }

    }
}

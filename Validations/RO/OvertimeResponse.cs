namespace PayrollAPI.Validations.RO
{
    public class OvertimeResponse
    {
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }
        public int staffId { get; set; }
    }
}
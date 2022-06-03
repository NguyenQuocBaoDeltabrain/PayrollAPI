namespace PayrollAPI.Validations
{
    public class OvertimeResponse
    {
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }
        public int staffId { get; set; }
    }
}
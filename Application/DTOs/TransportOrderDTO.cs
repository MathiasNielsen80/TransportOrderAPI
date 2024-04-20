namespace TransportOrderAPI.DTOs
{
    public class TransportOrderDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; }
    }
}

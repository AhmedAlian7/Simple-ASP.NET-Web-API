namespace Back_End.Models.DTOs
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

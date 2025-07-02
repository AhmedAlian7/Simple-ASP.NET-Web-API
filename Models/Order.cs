namespace Back_End.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // F_Key
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public Product? Product { get; set; } // Navigation Property
    }
}

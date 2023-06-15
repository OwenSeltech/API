namespace API.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber{ get; set; }
        public ProductType Type { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }

    }
    public enum ProductType
    {
        Card,
        Account
    }
}

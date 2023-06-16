namespace API.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string ProductNumber{ get; set; }
        public string ProductType  { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }
        public Customer Customer { get; set; }

    }
    public enum ProductTypeEnum
    {
        CARD,
        ACCOUNT
    }
}

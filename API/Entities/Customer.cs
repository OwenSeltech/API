namespace API.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmailAddress { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DateDeleted { get; set; }

    }
}

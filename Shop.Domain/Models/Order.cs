namespace Shop.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }

        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

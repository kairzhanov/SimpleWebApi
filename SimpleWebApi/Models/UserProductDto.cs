namespace SimpleWebApi.Models
{
    public class UserProductDto
    {
        public int UserProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
    }
}
namespace Entities.DTOs.ProductDTOs
{
    public class ProductSearchDTO
    {


        public int Id { get; set; }
        public string ProductTitle { get; set; }
        public string SeoUrl { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }

    }
}

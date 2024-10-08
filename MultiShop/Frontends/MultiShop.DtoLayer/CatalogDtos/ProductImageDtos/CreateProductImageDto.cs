namespace MultiShop.DtoLayer.CatalogDtos.ProductImageDtos
{
    public class CreateProductImageDto
    {
        public List<string> Image { get; set; } = new List<string>();
        public string ProductId { get; set; }
    }
}

﻿namespace MultiShop.Catalog.Dtos.ProductImageDtos
{
    public class UpdateProductImageDto
    {
        public string ProductImageId { get; set; }
        public List<string> Image { get; set; } = new List<string>();
        public string ProductId { get; set; }
    }
}

﻿namespace MultiShop.DtoLayer.CatalogDtos.ProductImageDtos
{
    public class ResultProductImageDto
    {
        public string ProductImageId { get; set; }
        public List<string> Image { get; set; } = new List<string>();
        public string ProductId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ScarpeAndCo.Models
{

    public class Product
    {
        private static List<Product> products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Scarpe Nike",
                    Price = 99.99m,
                    Description = "Scarpe da tennis di alta qualità, comode per ogni occasione.",
                    CoverImageBase64 = GetBase64Image("nike.jpg"),
                    Image1Base64 = GetBase64Image("scarpa1.jpg"),
                    Image2Base64 = GetBase64Image("scarpa2.jpg")
                }
            };

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CoverImageBase64 { get; set; }
        public string Image1Base64 { get; set; }
        public string Image2Base64 { get; set; }

        // Proprietà per file immagine (non vengono salvate, ma per caricare da form)
        public IFormFile CoverImageFile { get; set; }
        public IFormFile Image1File { get; set; }
        public IFormFile Image2File { get; set; }

        public static List<Product> GetProducts()
        {
            return products;
        }

        public static Product? GetProductById(int id)
        {
            return products.Find(p => p.Id == id);
        }

        private static string GetBase64Image(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            var imageBytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(imageBytes);
        }
    }
}

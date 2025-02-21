using Microsoft.AspNetCore.Mvc;
using ScarpeAndCo.Models;


namespace ScarpeCo.Controllers
{
   
        public class ProductController : Controller
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

            // Metodo per visualizzare tutti i prodotti
            public IActionResult Index()
            {
                return View(products);
            }

            // Metodo per visualizzare i dettagli di un prodotto
            public IActionResult Details(int id)
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }

            // Metodo per creare un nuovo prodotto
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(Product product)
            {
                if (ModelState.IsValid)
                {
                    // Gestione delle immagini in formato Base64
                    if (product.CoverImageFile != null)
                    {
                        product.CoverImageBase64 = ConvertToBase64(product.CoverImageFile);
                    }
                    if (product.Image1File != null)
                    {
                        product.Image1Base64 = ConvertToBase64(product.Image1File);
                    }
                    if (product.Image2File != null)
                    {
                        product.Image2Base64 = ConvertToBase64(product.Image2File);
                    }

                    product.Id = products.Max(p => p.Id) + 1;
                    products.Add(product);
                    return RedirectToAction(nameof(Index));
                }

                return View(product);
            }

            // Metodo per convertire un file immagine in Base64
            private string ConvertToBase64(IFormFile file)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var fileBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(fileBytes);
                }
            }

            // Metodo per caricare immagini di default per il sito
            private static string GetBase64Image(string fileName)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return Convert.ToBase64String(imageBytes);
            }
        }
    }


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
            },
            new Product
            {
                Id = 2,
                Name = "Scarpe Adidas",
                Price = 89.99m,
                Description = "Scarpe da corsa leggere e traspiranti, adatte per allenamenti intensi",
                CoverImageBase64 = GetBase64Image("adidas.jpg"),
                Image1Base64 = GetBase64Image("scarpa3.jpg"),
                Image2Base64 = GetBase64Image("scarpa4.jpg")
            },
            new Product
            {
                Id = 3,
                Name = "Scarpe Puma",
                Price = 79.99m,
                Description = "Scarpe da ginnastica con suola in gomma, adatte per ogni tipo di sport",
                CoverImageBase64 = GetBase64Image("puma.jpg"),
                Image1Base64 = GetBase64Image("scarpa5.jpg"),
                Image2Base64 = GetBase64Image("scarpa6.jpg")
            },
            new Product
            {
                Id = 4,
                Name = "Scarpe Reebok",
                Price = 69.99m,
                Description = "Scarpe da corsa con ammortizzazione, adatte per lunghe distanze",
                CoverImageBase64 = GetBase64Image("rebook.jpg"),
                Image1Base64 = GetBase64Image("scarpa7.jpg"),
                Image2Base64 = GetBase64Image("scarpa8.jpg")
            },
            new Product
            {
                Id = 5,
                Name = "Scarpe Asics",
                Price = 59.99m,
                Description = "Scarpe da tennis con suola antiscivolo, adatte per campi in terra battuta",
                CoverImageBase64 = GetBase64Image("asics.jpg"),
                Image1Base64 = GetBase64Image("scarpa9.jpg"),
                Image2Base64 = GetBase64Image("scarpa10.jpg")
            },
            new Product
            {
                Id = 6,
                Name = "Scarpe New Balance",
                Price = 49.99m,
                Description = "Scarpe da ginnastica con tomaia in mesh, adatte per il tempo libero",
                CoverImageBase64 = GetBase64Image("NewBalance.jpg"),
                Image1Base64 = GetBase64Image("scarpa11.jpg"),
                Image2Base64 = GetBase64Image("scarpa12.jpg")
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


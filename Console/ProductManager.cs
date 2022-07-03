using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console
{
    public class ProductManager : IProductService
    {
        private IProductDb ProductService { get; set; }
        
        public ProductManager(IProductDb productService)
        {
            ProductService = productService;
        }
        public void AddProduct(Product product)
        {
            ProductService.AddToDatabase();
            System.Console.WriteLine($"Ürün eklendi...");
        }
    }
}
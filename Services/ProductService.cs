using Angular_Test_App.Model;
using Angular_Test_App.Model.Shop;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular_Test_App.Services
{
    public class ProductService
    {
        private readonly WaxHandballAppDbContext _context;

        public ProductService(WaxHandballAppDbContext context)
        {
            _context = context;
        }

        public List<Product> GetProducts()
        {
            var result =  _context.Products.ToList();
            return result;
        }

        public async Task<Product> AddProduct(Product product)
        {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var selectedProduct = _context.Products.FirstOrDefault(product => product.ProductId == id);
            _context.Products.Remove(selectedProduct);
            await _context.SaveChangesAsync();
            return selectedProduct;

        }
    }
}

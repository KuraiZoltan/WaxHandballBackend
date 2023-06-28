using Angular_Test_App.Model.Shop;
using Angular_Test_App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Angular_Test_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("getProducts")]
        public IEnumerable<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        [HttpPost]
        [Authorize]
        [Route("addProduct")]
        public async Task<Product> AddProduct([FromBody]Product product)
        {
            return await _productService.AddProduct(product);
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteProduct/{id}")]
        public async Task<Product> DeleteProduct(int id)
        {
            return await _productService.DeleteProduct(id);
        }
    }
}
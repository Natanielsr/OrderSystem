using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSystem.Domain.Entities;
using OrderSystem.Domain.Repository;

namespace OrderSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepository productRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await productRepository.GetAll();
            var products = result.Select(e => (Product)e);
            return Ok(products);
        }
    }
}

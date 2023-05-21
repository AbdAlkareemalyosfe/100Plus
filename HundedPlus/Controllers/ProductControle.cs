using Base.Models;
using IRepostry.Model_Dto;
using Microsoft.AspNetCore.Mvc;
using Repostry.Repo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HundedPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductControle : ControllerBase
    {
        private readonly RepoProduct repoProduct;

        public ProductControle(RepoProduct repoProduct)
        {
            this.repoProduct = repoProduct;
        }

        // GET: api/<ProductControle>
        [HttpGet("GetAllProduct")]
       public async Task<IActionResult> GetAllProduct()
        {
            var products = await repoProduct.GetAllAsync(); 
            return Ok(products);
        }    

        // GET api/<ProductControle>/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromBody]int id)
        {
            var product =await repoProduct.GetByIdAsync(id);    
            return Ok(product);
           
        }

        // POST api/<ProductControle>
        [HttpPost("Add Product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDtoModel productDto)
        {
            var result = repoProduct.AddProduct(productDto);
            return Ok(result);
        }

        // PUT api/<ProductControle>/5
        [HttpPut(" Update Product{id}")]
        public async Task<IActionResult>UpdateProduct(ProductDtoUpAndDelt DtoModel)
        {
            var result = await repoProduct.UpdateProduct(DtoModel);
            return Ok(result);
        }

        // DELETE api/<ProductControle>/5
        [HttpPut(" Delet Product{id}")]
        public async Task<IActionResult> DeletProduct(int Id )
        {
           var result = repoProduct.DeletProduct(Id);
            if(result)
                return Ok( );
            return BadRequest();
        }
    }
}

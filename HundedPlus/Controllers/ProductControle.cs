using IRepostry.IRepo;
using IRepostry.Model_Dto;
using Microsoft.AspNetCore.Mvc;
using Shared_Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HundedPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductControle : ControllerBase
    {
        private readonly IRepoProduct repoProduct;

        public ProductControle(IRepoProduct repoProduct)
        {
            this.repoProduct = repoProduct;
        }

        // GET: api/<ProductControle>
        [HttpGet("GetAllProduct")]
       public async Task<IActionResult> GetAllProduct()
        {
            var products = await repoProduct.GetAllAsync();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products.ToList());

        }    

        // GET api/<ProductControle>/5
        [HttpGet("GetByIdProduct")]
        public async Task<IActionResult> GetById(int Id)
        {
            var product =await repoProduct.GetByIdAsync(Id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
           
        }

        // POST api/<ProductControle>
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDtoModel productDto)
        {
            var result = await repoProduct.AddProduct(productDto);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };
        }

        // PUT api/<ProductControle>/5
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult>UpdateProduct(ProductDtoUp DtoModel)
        {
            var result = await repoProduct.UpdateProduct(DtoModel);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };
        }

        // DELETE api/<ProductControle>/5
        [HttpPut("DeletProduct")]
        public async Task<IActionResult> DeletProduct(int Id )
        {
           
           var result = repoProduct.DeletProduct(Id);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };
        }
    }
}

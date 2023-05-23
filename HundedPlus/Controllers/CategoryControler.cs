using IRepostry.IRepo;
using IRepostry.Model_Dto;
using Microsoft.AspNetCore.Mvc;
using Repostry.Repo;
using Shared_Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HundedPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryControler : ControllerBase
    {
        private readonly IRepoCategory _repoCategory;

        public CategoryControler(IRepoCategory repoCategory)
        {
            _repoCategory = repoCategory;
        }

        // GET: api/<CategoryControler>
        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var results = await _repoCategory.GetAllAsync();
            if (results == null)
            {
                return BadRequest(string.Empty);
            }
            return Ok(results.ToList());
           
        }

        // GET api/<CategoryControler>/5
        [HttpGet("GetCategoryById{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var result = await _repoCategory.GetByIdAsync(id);
            if (result == null)
            { return NotFound(); }
            return Ok(result);

        }

        // POST api/<CategoryControler>
        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory( CategoryDtoModel categoryDto)
        {
            var result = await _repoCategory.CreatCatogryAsync(categoryDto);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };
        }

        // PUT api/<CategoryControler>/5
        [HttpPut("UpdateCategory{id}")]
        public async Task<IActionResult>UpdateCategory([FromBody]CategoryDtoUpdate   categoryDto)
        {
            var result = await _repoCategory.UpdateCatogryAsync(categoryDto);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };
        }

        // DELETE api/<CategoryControler>/5
        [HttpPut("DeletCategoryById{id}")]
        public  async Task<IActionResult> DeleteCategory(int id)
        {
            var result =await  _repoCategory.DeletCaategory(id);
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

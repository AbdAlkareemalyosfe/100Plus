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
    public class OrderController : ControllerBase
    {
        private readonly IRepoOrder _repoOrder;

        public OrderController(IRepoOrder repoOrder)
        {
           _repoOrder = repoOrder;
        }

        // GET: api/<OrderController>
        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder()
        {
            var results = await _repoOrder.GetAllOreders();
            if (results == null)
            {
                return BadRequest(string.Empty);
            }
            return Ok(results.RangeResults);
        }

        // GET api/<OrderController>/5
        [HttpGet("GetOrderById{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var results = await _repoOrder.GetOrderById(id);
            if (results == null)
            {
                return BadRequest(string.Empty);
            }
            return Ok(results.Result);
        }

        // POST api/<OrderController>
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] OrderdtoModel orderdtoModel)
        {
            var result = await _repoOrder.CreatOrder(orderdtoModel);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };
        }

        // PUT api/<OrderController>/5
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult>  UpdateOrder(OrderdtoUpdate orderdtoUpdate)
        {
            var result = await _repoOrder.UpdateOrder(orderdtoUpdate);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };
        }

        // DELETE api/<OrderController>/5
        [HttpPut("DeletOrder")]
        public async Task<IActionResult> DeletOrder(int id)
        {
            var result = await _repoOrder.DeletOrder(id);
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

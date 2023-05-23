using Base;
using IRepostry.IRepo;
using IRepostry.Model_Dto;
using Microsoft.AspNetCore.Mvc;
using Shared_Core;
using Twilio.TwiML.Voice;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HundedPlus.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class OfferControler : ControllerBase
    {
        private readonly IRepoOffer _repoOffer;

        public OfferControler(IRepoOffer repoOffer)
        {
            _repoOffer = repoOffer;
        }


        // GET: api/<OfferControler>
        [HttpGet("GetAllOffers")]
        public async Task<IActionResult>GetAllOffers() 
        {
           var results = await _repoOffer.GetAllAsync();
            if (results== null) 
            { return NotFound(); }
            return Ok(results.ToList());
        }

        // GET api/<OfferControler>/5
        [HttpGet("GetOfferById")]
        public async Task<IActionResult> GetOfferById(int id)
        {
           var result =await _repoOffer.GetByIdAsync(id);
            if(result==null) 
            { return NotFound(); } 
            return Ok(result);
        }

        // POST api/<OfferControler>
        [HttpPost ("AddOffer")]
        public async Task<IActionResult> AddOffer([FromBody]OfferDtoModel offerDto)
        {
            var result =await _repoOffer.AddOffer(offerDto);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };

        }

        // PUT api/<OfferControler>/5
        [HttpPut("UpdateOffer")]
        public async Task<IActionResult> UpdateOffer(OfferDtoUp offerDto)
        {
            var result = await _repoOffer.UpdateOffer(offerDto);
            switch (result.OperrationResultType)
            {
                case OperationResultType.Exception:
                    return new JsonResult("Exception") { StatusCode = 400 };
                case OperationResultType.Success:
                    return new JsonResult(result.Result) { StatusCode = 200 };
            }
            return new JsonResult("Unknown Error") { StatusCode = 500 };
        }

        // DELETE api/<OfferControler>/5
        [HttpPut("DeletOffer")]
        public async Task<IActionResult> DeletOffer(int id)
        {
            var result = _repoOffer.DeletOffer(id);
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

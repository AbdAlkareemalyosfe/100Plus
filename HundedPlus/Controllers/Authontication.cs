using IRepostry.IRepo;
using IRepostry.Model_Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Shared_Core;

namespace HundedPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authontication : ControllerBase
    {
        private readonly IRepoRegister register;
       

        public Authontication(IRepoRegister register )
        {
            this.register = register;
           
        }
        [HttpPost("verfiction_1")]
        public async  Task<IActionResult> AddNumber( string PhonNumber )
        {
            // Verify the phone number and get the verification code
            var result = await  register.verification(PhonNumber);
            var verificationResult =  result.Message;
            if(result.OperrationResultType==OperationResultType.Success)
            {
                return Ok("Send verficate Is Success");
            }
            return Ok (result.Message);
        }
        [HttpPost("verification_2")]
        public async Task<IActionResult> AddCodeVerification(string phoneNumber, string verificationCode)
        {
            if (phoneNumber == null || verificationCode == null)
            {
                return BadRequest("Please enter the verification code");
            }

            var user = await register.FindUserByPhone(phoneNumber);

            if (user == null)
            {
                // User is not registered, so return a 404 Not Found status code
                return NotFound();
            }
            else
            {
                // User is already registered, so log in the user
                var result = await register.LogInAsync(phoneNumber);

                if (result.OperrationResultType==OperationResultType.Success)
                {
                    return Ok("User logged in successfully");
                }
                else
                {
                    return BadRequest("Failed to log in user");
                }
            }
        }
        [HttpPost("RegisterNewAccount")]
        public async Task<IActionResult> Register (RegisterUsrDto usrDto)
        {
            var result = await  register.RegisterUserAsync(usrDto);
            if(result.OperrationResultType== OperationResultType.Success)
                return Ok(" Register Is Success");
            else
               return BadRequest("Register Is Failed"); 
        }


    }
    
}

using Azure;
using Base;
using Base.Models;
using IRepostry.IRepo;
using IRepostry.Model_Dto;
using IRepostry.Model_Respons;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Jwt.AccessToken;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Repostry.Repo
{
    public class RepoRegister : IRepoRegister
    {
        private readonly HundredPlusDbContext _context;
        private readonly IDistributedCache _cache;

        public RepoRegister(HundredPlusDbContext context , IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<OperationResult<UserInfoRespons>> RegisterUserAsync(RegisterUsrDto DtoUser)
        {
          OperationResult<UserInfoRespons> operation =new OperationResult<UserInfoRespons>();
            try
            {
                
                var user = await _context.Users.Where(x =>x.PhoneNumber == DtoUser.PhoneNumber).FirstOrDefaultAsync();
                
                var UserAdd = new User
                {
                    Token = GenerateToken(DtoUser.PhoneNumber),
                    PhoneNumber = DtoUser.PhoneNumber,
                    FirstName = DtoUser.FirstName,
                    LastName = DtoUser.LastName

                };
                await _context.Users.AddAsync(UserAdd);
                _context.SaveChanges();

                operation.Result = new UserInfoRespons
                {
                    FirstName = UserAdd.FirstName,
                    LastName = UserAdd.LastName,
                    PhoneNumber = UserAdd.PhoneNumber,
                    Id = UserAdd.Id,
                    token = UserAdd.Token
                };
                operation.OperrationResultType = OperationResultType.Success;
               
            }
            catch (Exception ex)
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<bool>> verification(string PhoneNumber)
        {
            OperationResult<bool> operation =new OperationResult<bool>();
            try
            {
                // Your Twilio account SID and auth token, obtained from the Twilio Console
                const string accountSid = "YOUR_ACCOUNT_SID";
                const string authToken = "YOUR_AUTH_TOKEN";

                // The phone number to send the verification code to
                string phoneNumber = PhoneNumber;

                // The verification code to send to the user
                string verificationCode = GetFiveNumberRandom();

                // Initialize the Twilio client with your account SID and auth token
               
                TwilioClient.Init(accountSid, authToken);

                // Store the verification code in the cache with the phone number as the key
                await _cache.SetStringAsync(phoneNumber, verificationCode, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) 
                    // Expire the verification code after 5 minutes
                });

                // Send an SMS message with the verification code to the user's phone number
                var message = await MessageResource.CreateAsync(
                    body: $"Your verification code is: {verificationCode}",
                    from: new Twilio.Types.PhoneNumber("+14158434465"), // Your Twilio phone number
                    to: new Twilio.Types.PhoneNumber(phoneNumber)
                );

                // Check the status of the SMS message and handle any errors
                if (message.Status == MessageResource.StatusEnum.Failed || message.Status == MessageResource.StatusEnum.Undelivered)
                {
                    operation.OperrationResultType = OperationResultType.Failed;
                    operation.Result = false;
                    operation.Message = message.ErrorMessage;

                }
                else
                {
                    // SMS message sent successfully
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.Result = true;
                    operation.Message = verificationCode;
                }
            }
            catch (Exception ex)
            {
                operation.Exception = ex;
                operation.OperrationResultType= OperationResultType.Exception;
            }
            return operation;
        }
        private  string GetFiveNumberRandom()
        {
            Random random = new Random();
            List<int> randomNumbers = new List<int>();

            for (int i = 0; i < 5; i++)
            {
                int randomNumber = random.Next(0, 10);
                 randomNumbers.Add(randomNumber);
            }

            string result = string.Join(",", randomNumbers);
            return result;
        }
        private string GenerateToken (string PhonNumber)
        {
            // Define the payload as a set of claims
            var claims = new[]
            {
            new Claim(ClaimTypes.MobilePhone,PhonNumber),
            new Claim(ClaimTypes.Role,"User")
            };

            // Define the secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretkey"));

            // Define the signing credentials
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // Define the JWT token
            var token = new JwtSecurityToken(
                issuer: "myapp.com",
                audience: "myapp.com",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials
            );

            // Encode the JWT token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<OperationResult<UserInfoRespons>> LogInAsync (string PhonN)
        {
            OperationResult<UserInfoRespons> operation =new OperationResult<UserInfoRespons>();
            try
            {
                var user = await _context.Users.Where(x => x.PhoneNumber == PhonN).SingleOrDefaultAsync();
                
                 user.Token = GenerateToken(PhonN) ;
                 _context.Update(user);
                _context.SaveChanges();
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = new UserInfoRespons
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id,
                    token = user.Token
                };

            }
            catch (Exception ex) 
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public  bool PhoneIsFound(string Phone) 
        {
            var result = _context.Users.Where(P=>P.PhoneNumber==Phone).FirstOrDefault();
            if (result == null)
            {
                return false;
            }
            return true;
        }
        private async Task<string> GetValidVerificationCode(string phoneNumber)
        {

            var cachedVerificationCodeBytes = await _cache.GetAsync(phoneNumber);
            if (cachedVerificationCodeBytes != null && cachedVerificationCodeBytes.Length > 0)
            {
                var cachedVerificationCode = Encoding.UTF8.GetString(cachedVerificationCodeBytes);
                return cachedVerificationCode;
            }
            else
            {
               // The cached verification code has expired or does not exist
                    return null;
            }
        }
        public async Task<bool> verficate(string verficatDto, string Phone)
        {

            var storedVerificationCode = await _cache.GetAsync(Phone);
            var cachedVerificationCode = Encoding.UTF8.GetString(storedVerificationCode);
            if (cachedVerificationCode == verficatDto)
            {
                // Verification code is valid, proceed with the next steps
                return true;
            }
            else
            {
                // Verification code is invalid, return an error message
                return false;
            }
        }
        public async Task<bool> NumberNotFounf(string Phone)
        {
            var user =await _context.Users.FindAsync(Phone);
            if (user != null) return true; 
            return false;
        }
        public async Task<User> FindUserByPhone(string Phone)
        {
            return await _context.Users.FindAsync(Phone);
        }

    }


}


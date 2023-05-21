using Base.Models;
using IRepostry.Model_Dto;
using IRepostry.Model_Respons;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.IRepo
{
    public interface IRepoRegister
    {
       Task<OperationResult<UserInfoRespons>> RegisterUserAsync(RegisterUsrDto DtoUser);
       Task<OperationResult<bool>> verification(string PhoneNumber);
       Task<OperationResult<UserInfoRespons>> LogInAsync(string PhonN);
       bool PhoneIsFound(string PhoneNumber);
       Task<bool> verficate(string verficatDto, string Phone);
       Task<bool> NumberNotFounf(string Phone);
       Task<User> FindUserByPhone(string Phone);

    }
}

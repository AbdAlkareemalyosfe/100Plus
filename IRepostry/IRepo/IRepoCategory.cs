using Base.Models;
using IRepostry.Model_Dto;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.IRepo
{
    public interface IRepoCategory : IBaseRepostry<Category> 
    {
        Task<OperationResult<Category>> CreatCatogryAsync(CategoryDtoModel categoryDto);
        Task<OperationResult<Category>> UpdateCatogryAsync(CategoryDtoUpdate categoryDto);
        Task <OperationResult<bool>>  DeletCaategory (int Id);
     }
}

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
    public interface IRepoCategory 
    {
        Task<OperationResult<CategoryResponceInfo>> CreatCatogryAsync(CategoryDtoModel categoryDto);
        Task<OperationResult<CategoryResponceInfo>> UpdateCatogryAsync(CategoryDtoUpdate categoryDto);
        Task <OperationResult<bool>>  DeletCaategory (int Id);
        Task<OperationResult<dynamic>> GetAllCategories();
        Task<OperationResult<dynamic>> GetGategoryById(int id);

     }
}

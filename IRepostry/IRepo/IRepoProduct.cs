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
    public interface IRepoProduct 
    {
        Task<OperationResult<ProductResponceInfo>> AddProduct(ProductDtoModel productDto);
        OperationResult<bool> DeletProduct(int Id);
        Task<OperationResult<ProductResponceInfo>> UpdateProduct(ProductDtoUp productDto);
        Task<OperationResult<dynamic>> GetAllProducts();
        Task<OperationResult<dynamic>> GetProductById(int id);



    }
}

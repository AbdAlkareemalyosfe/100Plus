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
    public interface IRepoProduct : IBaseRepostry<Product>
    {
        Task<OperationResult< Product>> AddProduct(ProductDtoModel productDto);
        OperationResult<bool> DeletProduct(int Id);
        Task<OperationResult<Product>> UpdateProduct(ProductDtoUp productDto);

    }
}

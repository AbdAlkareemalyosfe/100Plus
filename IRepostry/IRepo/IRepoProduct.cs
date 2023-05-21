using Base.Models;
using IRepostry.Model_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.IRepo
{
    public interface IRepoProduct : IBaseRepostry<Product>
    {
        Task<Product> AddProduct(ProductDtoModel productDto);

    }
}

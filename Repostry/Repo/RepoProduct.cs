using Base;
using Base.Models;
using IRepostry.IRepo;
using IRepostry.Model_Dto;
using Microsoft.EntityFrameworkCore;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostry.Repo
{
    public class RepoProduct : BaseRepostry<Product>, IRepoProduct
    {
        private readonly HundredPlusDbContext _context;
        public RepoProduct(HundredPlusDbContext context) : base(context)
        {
            _context =context ;
        }
        public async Task<OperationResult< Product>> AddProduct(ProductDtoModel productDto)
        {
            OperationResult<Product> operation = new OperationResult<Product>();
            try
            {
                Product product = new Product()
                {
                    Name = productDto.Name,
                    Price = productDto.Price,
                    Description = productDto.Description ?? ""
                };
                var result = await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                operation.OperrationResultType=OperationResultType.Success;
                operation.Result = product;
            }
            catch(Exception ex) 
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<Product>> UpdateProduct(ProductDtoUp productDto)
        {
            OperationResult<Product> operation = new OperationResult<Product>();
            try
            {
                var product = await _context.Products.Where(p => p.Id == productDto.Id).SingleOrDefaultAsync();
                if (product != null)
                {
                    if (product != null)
                    {
                        if (productDto.Price != null)
                        {
                            product.Price = productDto.Price;
                        }
                        if (productDto.Description != null)
                        {
                            product.Description = productDto.Description;
                        }
                        if (productDto.Name != null)
                        {
                            product.Name = productDto.Name;
                        }
                        _context.Products.Update(product);
                        await _context.SaveChangesAsync();
                    }
                }
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = product;
            }
            catch( Exception ex )   
            {
                operation.OperrationResultType=OperationResultType.Exception;
                operation.Exception = ex;
            }

            return operation;
        }
        public OperationResult<bool> DeletProduct(int Id)
        {
            OperationResult<bool> operation = new OperationResult<bool>();
            try
            {
                var product = _context.Products.Where(p => p.Id == Id).SingleOrDefault();
                if (product != null)
                {
                    //Product newPrpduct = new Product()
                    //{
                    //    Id = Id,
                    //    IsDeleted = true,
                    //    DatetimeDeleted = DateTime.Now,
                    //};
                    product.IsDeleted = true;
                    product.DatetimeDeleted = DateTime.UtcNow;
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.Result = true;

                }
                else
                {
                    operation.OperrationResultType = OperationResultType.Failed;
                    operation.Result = false;
                }

            }
            catch( Exception ex )
            {
                operation.OperrationResultType=OperationResultType.Exception;
                operation.Exception = ex;
            }

            return operation;
        }
    }
}

using Base;
using Base.Models;
using IRepostry.IRepo;
using IRepostry.Model_Dto;
using IRepostry.Model_Respons;
using Microsoft.EntityFrameworkCore;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostry.Repo
{
    public class RepoProduct : IRepoProduct
    {
        private readonly HundredPlusDbContext _context;
        public RepoProduct(HundredPlusDbContext context)
        {
            _context = context;
        }
        public async Task<OperationResult<ProductResponceInfo>> AddProduct(ProductDtoModel productDto)
        {
            OperationResult<ProductResponceInfo> operation = new OperationResult<ProductResponceInfo>();
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
                ProductResponceInfo productResponceInfo = new ProductResponceInfo()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description ?? ""
                };
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = productResponceInfo;
            }
            catch (Exception ex)
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<ProductResponceInfo>> UpdateProduct(ProductDtoUp productDto)
        {
            OperationResult<ProductResponceInfo> operation = new OperationResult<ProductResponceInfo>();
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
                        ProductResponceInfo productResponceInfo = new ProductResponceInfo()
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            Description = product.Description ?? ""
                        };
                        operation.OperrationResultType = OperationResultType.Success;
                        operation.Result = productResponceInfo;
                    }
                    else
                    {
                        operation.OperrationResultType = OperationResultType.Failed;
                        operation.Result = null;
                    }
                }

            }
            catch (Exception ex)
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }

            return operation;
        }
        public OperationResult<bool> DeletProduct(int Id)
        {
            OperationResult<bool> operation = new OperationResult<bool>();
            try
            {
                var product = _context.Products.Where(p => p.Id == Id && !p.IsDeleted).SingleOrDefault();
                if (product == null)
                {
                    operation.OperrationResultType = OperationResultType.NotExit;
                    operation.Result = false;
                    return operation;
                }
                else if (product.IsDeleted)
                {
                    operation.OperrationResultType = OperationResultType.previouslyDeleted;
                    operation.Result = false;
                }
                else
                {

                    product.IsDeleted = true;
                    product.DatetimeDeleted = DateTime.UtcNow;
                    _context.Products.Update(product);
                    _context.SaveChanges();
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.Result = true;

                }
            }
            catch (Exception ex)
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }

            return operation;
        }

        public async Task<OperationResult<dynamic>> GetAllProducts()
        {
            OperationResult<dynamic> operation = new OperationResult<dynamic>();
            try
            {
                

                var result = await _context.Products.Where(d => !d.IsDeleted)
                    .Select(o => new { o.Id, o.Name, o.Description, o.Price })
                .ToListAsync();
                if (result.Count > 0)
                {
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.RangeResults = result;
                }
                else
                {
                    operation.OperrationResultType = OperationResultType.Failed;
                    operation.Result = null;
                }
            }
            catch (Exception ex)
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<dynamic>> GetProductById(int id)
        {
            OperationResult<dynamic> operation = new OperationResult<dynamic>();
            try
            {
                var result = await _context.Products.Where(d => !d.IsDeleted && d.Id == id)
                     .Select(o => new { o.Id, o.Name, o.Description, o.Price })
                     .SingleOrDefaultAsync();
                if (result != null)
                {
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.Result = result;
                }
                else
                {
                    operation.OperrationResultType = OperationResultType.Failed;
                    operation.Result = null;
                }
            }
            catch (Exception ex)
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
    }
}

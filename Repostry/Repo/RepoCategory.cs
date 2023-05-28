using Base;
using Base.Models;
using IRepostry;
using IRepostry.IRepo;
using IRepostry.Model_Dto;
using IRepostry.Model_Respons;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repostry.Repo
{
    public class RepoCategory : IRepoCategory
    {
        private readonly HundredPlusDbContext _context;

        public RepoCategory(HundredPlusDbContext context) 
        {
            _context = context;
        }

        public async Task<OperationResult<CategoryResponceInfo>> CreatCatogryAsync(CategoryDtoModel categoryDto)
        {
            double costCategory = 0;

            OperationResult<CategoryResponceInfo> operation = new OperationResult<CategoryResponceInfo>();
            try
            {


                if (categoryDto.ProductId != null && categoryDto.ProductId !=0 ) 
                {
                    // Retrieve the Product entity based on the ProductId property of the CategoryDtoModel
                    categoryDto.OfferId = null;
                    Product product = _context.Products.FirstOrDefault(c => c.Id == categoryDto.ProductId); ;


                    if (product != null && !product.IsDeleted)
                    {
                        // Calculate the CostCategory property based on the Price of the Product entity and the Quantity of the CategoryDtoModel
                        costCategory = product.Price * categoryDto.Quantity;
                    }
                    else
                    {
                        operation.Message = $"Product with ID {categoryDto.ProductId} not found.";
                        operation.OperrationResultType = OperationResultType.NotExit;
                        return operation;
                    }
                }
                else if (categoryDto.OfferId != null && categoryDto.OfferId !=0)
                {
                    categoryDto.ProductId = null;  
                    // Retrieve the Offer entity based on the OfferId property of the CategoryDtoModel
                    Offer offer = await _context.Offers.FindAsync(categoryDto.OfferId);

                    if (offer != null)
                    {
                        // Calculate the CostCategory property based on the NewPrice of the Offer entity and the Quantity of the CategoryDtoModel
                        costCategory = offer.NewPrice * categoryDto.Quantity;
                    }
                    else
                    {
                        operation.Message = $"Offer with ID {categoryDto.OfferId} not found.";
                        operation.OperrationResultType = OperationResultType.NotExit;
                        return operation;
                    }
                }

                Category category = new Category()
                {
                    Quantity = categoryDto.Quantity,
                    ProductId = categoryDto.ProductId,
                    OfferId = categoryDto.OfferId ,
                    CostCategory = costCategory
                };

                _context.Categores.Add(category);
                await _context.SaveChangesAsync();

                CategoryResponceInfo categoryResponceInfo = new CategoryResponceInfo()
                {
                    Id= category.Id,
                    Quantity = categoryDto.Quantity,
                    ProductId = categoryDto.ProductId,
                    OfferId = categoryDto.OfferId,
                    CostCategory = costCategory

                };
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = categoryResponceInfo;
            }
            catch (Exception ex)
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }


        public async Task<OperationResult<CategoryResponceInfo>> UpdateCatogryAsync(CategoryDtoUpdate categoryDto)
        {
            OperationResult<CategoryResponceInfo> operation = new OperationResult<CategoryResponceInfo>();
            try
            {
                var category = await _context.Categores.Where(c => c.Id == categoryDto.Id).SingleOrDefaultAsync();
                if (category == null)
                {
                    operation.OperrationResultType = OperationResultType.NotExit;
                    operation.Result = null;
                }
                else if (!category.IsDeleted)
                {
                    if (categoryDto.ProductId != null || categoryDto.ProductId != 0)
                    {
                        category.ProductId = categoryDto.ProductId;
                    }
                    if (categoryDto.OfferId != null || categoryDto.OfferId != 0)
                    {
                        category.OfferId = categoryDto.OfferId;
                    }
                    if (categoryDto.Quantity != null || categoryDto.Quantity != 0)
                    {
                        category.Quantity = categoryDto.Quantity;
                    }
                    _context.Categores.Update(category);
                    await _context.SaveChangesAsync();
                    CategoryResponceInfo categoryResponceInfo = new CategoryResponceInfo()
                    {
                        Id = category.Id,
                        CostCategory = category.CostCategory,
                        OfferId = category.OfferId,
                        ProductId = category.ProductId,
                        Quantity = category.Quantity
                        
                    };
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.Result = categoryResponceInfo;
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
        public async  Task<OperationResult<bool>> DeletCaategory(int Id)
        {
            OperationResult<bool> operation = new OperationResult<bool>();
            try
            {
                
                var category = _context.Categores.Where(c => c.Id == Id ).SingleOrDefault();
                if (category == null)
                {
                    operation.OperrationResultType = OperationResultType.NotExit;
                    operation.Result = false;
                    return operation;
                }
                else if (category.IsDeleted)
                {
                    operation.OperrationResultType = OperationResultType.previouslyDeleted;
                    operation.Result = false;
                }
                else
                {
                    category.IsDeleted = true;
                    category.DatetimeDeleted = DateTime.UtcNow;
                    _context.Categores.Update(category);
                    await _context.SaveChangesAsync();
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.Result = true;
                }
            }
            catch(Exception ex)  
            {
                operation.OperrationResultType=OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<dynamic>> GetAllCategories() 
        {
            OperationResult<dynamic> operation = new OperationResult<dynamic>();
            try
            {
                
                var result = await _context.Categores.Where(d => !d.IsDeleted).Include(p=>p.Product).Include(o=>o.Offer)
                    .Select( s=>new {
                        Id = s.Id,
                        ProductId = s.ProductId,
                        Price = s.Product != null && s.Product.Price != null ? s.Product.Price : (double?)null,
                        OfferId = s.OfferId,
                        NewPrice = s.Offer != null && s.Offer.NewPrice != null ? s.Offer.NewPrice : (double?)null,
                        Quantity = s.Quantity,
                        CostCategory = s.CostCategory
                    })
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
                operation.OperrationResultType=OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<dynamic>> GetGategoryById(int id )
        {
            OperationResult<dynamic> operation = new OperationResult<dynamic>();
            try
            {
                var result = await _context.Categores.Where(d => !d.IsDeleted && d.Id==id ).Include(p => p.Product).Include(o => o.Offer)
                      .Select(s => new {
                          Id = s.Id,
                          ProductId = s.ProductId,
                          Price = s.Product != null && s.Product.Price != null ? s.Product.Price : (double?)null,
                          OfferId = s.OfferId,
                          NewPrice = s.Offer != null && s.Offer.NewPrice != null ? s.Offer.NewPrice : (double?)null,
                          Quantity = s.Quantity,
                          CostCategory = s.CostCategory
                      }).SingleOrDefaultAsync();
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

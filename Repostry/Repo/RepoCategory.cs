using Base;
using Base.Models;
using IRepostry;
using IRepostry.IRepo;
using IRepostry.Model_Dto;
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
    public class RepoCategory : BaseRepostry<Category>, IRepoCategory
    {
        private readonly HundredPlusDbContext _context;

        public RepoCategory(HundredPlusDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OperationResult<Category>> CreatCatogryAsync(CategoryDtoModel categoryDto)
        {
            double costCategory = 0;

            OperationResult<Category> operation = new OperationResult<Category>();
            try
            {


                if (categoryDto.ProductId != null && categoryDto.ProductId !=0) 
                {
                    // Retrieve the Product entity based on the ProductId property of the CategoryDtoModel
                    categoryDto.OfferId = 0;
                    Product product = await _context.Products.FindAsync(categoryDto.ProductId);


                    if (product != null)
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
                    categoryDto.ProductId = 0;  
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
                    OfferId = categoryDto.OfferId,
                    CostCategory = costCategory
                };

                _context.Categores.Add(category);
                await _context.SaveChangesAsync();

                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = category;
            }
            catch (Exception ex)
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }


        public async Task<OperationResult<Category>> UpdateCatogryAsync(CategoryDtoUpdate categoryDto)
        {
            OperationResult<Category> operation = new OperationResult<Category>();
            try
            {
                var category = await _context.Categores.Where(c => c.Id == categoryDto.Id).SingleOrDefaultAsync();
                if (category == null)
                {
                    operation.OperrationResultType = OperationResultType.NotExit;
                    operation.Result = null;
                }
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
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = category;
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
            var category =  _context.Categores.Where(c => c.Id == Id).SingleOrDefault();
            if (category == null)
            {
                operation.OperrationResultType = OperationResultType.NotExit;
                operation.Result = false;
            }
            _context.Categores.Remove(category);
             _context.SaveChangesAsync();
            operation.OperrationResultType = OperationResultType.Success;
            operation.Result = true;

            return operation;
        }
    }
}

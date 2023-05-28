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
using Twilio.Types;

namespace Repostry.Repo
{
    public  class RepoOrder : IRepoOrder
    {
        private  readonly HundredPlusDbContext   _context;

        public RepoOrder(HundredPlusDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult<OrderResponceInfo>> CreatOrder (OrderdtoModel orderDto)
        {
            OperationResult<OrderResponceInfo> operation =new OperationResult<OrderResponceInfo>();
            try 
            { 
                var user = await _context.Users.Where(u=>u.Id==orderDto.UserId && !u.IsDeleted).SingleOrDefaultAsync();
                // if (user != null && orderDto.CategoryIds.All(id => _context.Categores.Contains(id)))
                if (user != null && orderDto.CategoryIds.All(id => _context.Categores.Any(c => c.Id == id)))
                {
                   // Check if the user already has an open order
                   Order? order =  _context.Orders
                      .Include(o => o.Categories)
                      .Where(o => o.UserId == orderDto.UserId && 
                     o.Status==OrderStatus.Processing &&
                      !o.IsDeleted).SingleOrDefault();
                   if (order == null)
                   {
                        long PartialBalance;

                        // If the user doesn't have an open order, create a new one
                        Order neworder = new Order
                        {
                            UserId = orderDto.UserId,
                            Status = OrderStatus.Processing,
                            Categories = new List<Category>(),
                            PhoneNumberOfUser = user.PhoneNumber

                        };
                        List<CategoryResponceInfo> categoryResponceInfos = new List<CategoryResponceInfo>();
                        foreach (var id in orderDto.CategoryIds)
                        {
                            neworder.Categories.Add(_context.Categores.FirstOrDefault(c => c.Id == id));
                            Category category = _context.Categores.SingleOrDefault(c => c.Id == id);

                            if (category != null)
                            {
                                CategoryResponceInfo categoryResponceInfo = new CategoryResponceInfo
                                {
                                    Id = category.Id,
                                    Quantity = category.Quantity,
                                    CostCategory = category.CostCategory,
                                    ProductId = category.ProductId,
                                    OfferId = category.OfferId
                                };

                                categoryResponceInfos.Add(categoryResponceInfo);
                            }
                        };
                        // Update the Partial price of the order
                        neworder.PartialBalance = (long)neworder.Categories.Sum(c => c.CostCategory);

                        await _context.Orders.AddAsync(neworder);
                        await _context.SaveChangesAsync();
                       

                     


                        OrderResponceInfo ResponceOrder = new OrderResponceInfo
                        {
                            Id = neworder.Id,
                            CostDilivary   =neworder.CostDilivary,
                            PhoneNumber = neworder.PhoneNumberOfUser,
                            PartialBalance =    neworder.PartialBalance,
                            TotalBalance = (long)neworder.PartialBalance + (long)neworder.CostDilivary,
                            UserId = neworder.UserId,
                            categories= categoryResponceInfos,
                            OrderStatus = neworder.Status.ToString()
                            
                        };
                        operation.OperrationResultType = OperationResultType.Success;
                        operation.Result = ResponceOrder;
                   }
                    else
                    {
                        List<CategoryResponceInfo> categoryResponceInfos = new List<CategoryResponceInfo>();
                        foreach (var id in orderDto.CategoryIds)
                        {
                            order.Categories.Add(_context.Categores.FirstOrDefault(c => c.Id == id));
                            Category category = _context.Categores.SingleOrDefault(c => c.Id == id);

                            if (category != null)
                            {
                                CategoryResponceInfo categoryResponceInfo = new CategoryResponceInfo
                                {
                                    Id = category.Id,
                                    Quantity = category.Quantity,
                                    CostCategory = category.CostCategory,
                                    ProductId = category.ProductId,
                                    OfferId = category.OfferId
                                };

                                categoryResponceInfos.Add(categoryResponceInfo);
                            }
                        };

                        order.PartialBalance = (long)order.Categories.Sum(c => c.CostCategory);
                        await _context.Orders.AddAsync(order);

                        OrderResponceInfo ResponceOrder = new OrderResponceInfo
                        {
                            Id = order.Id,
                            PhoneNumber = user.PhoneNumber,
                            PartialBalance = order.PartialBalance,
                            TotalBalance = order.PartialBalance + order.PartialBalance,
                            UserId = order.UserId,
                            categories = categoryResponceInfos,
                            OrderStatus = order.Status.ToString(),
                        };
                        operation.OperrationResultType = OperationResultType.Success;
                        operation.Result = ResponceOrder;
                    }
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
       public async Task<OperationResult<OrderResponceInfo>> UpdateOrder (OrderdtoUpdate orderDto)
       {
            OperationResult<OrderResponceInfo> operation = new OperationResult<OrderResponceInfo>();
            try
            {
                var order = _context.Orders.Include(c => c.Categories).FirstOrDefault(o => o.Id == orderDto.Id && !o.IsDeleted);
                if (order != null)
                {
                    if (order.UserId != orderDto.UserId)
                        order.UserId = orderDto.UserId;
                    if (orderDto.CategoryIds.Count >= 1 && orderDto.CategoryIds[0] != 0)
                    {
                        if (orderDto.CategoryIds.Any(categoryId => !_context.Categores.Any(c => c.Id == categoryId)))
                        {
                            operation.OperrationResultType = OperationResultType.NotExit;
                            operation.Message = "One or more CategoryIds are not valid.";
                            operation.Result = null;
                        }
                        order.Categories.Clear();
                        foreach (var categoryId in orderDto.CategoryIds)
                        {
                            order.Categories.Add(_context.Categores.FirstOrDefault(c => c.Id == categoryId));
                        }

                    }
                   _context.Orders.Update(order);
                    await _context.SaveChangesAsync();


                    List<CategoryResponceInfo> categoryResponceInfos = new List<CategoryResponceInfo>();
                    foreach (var id in orderDto.CategoryIds)
                    {
                        Category category = _context.Categores.SingleOrDefault(c => c.Id == id);

                        if (category != null)
                        {
                            CategoryResponceInfo categoryResponceInfo = new CategoryResponceInfo
                            {
                                Id = category.Id,
                                Quantity = category.Quantity,
                                CostCategory = category.CostCategory,
                                ProductId = category.ProductId,
                                OfferId = category.OfferId
                            };

                            categoryResponceInfos.Add(categoryResponceInfo);
                        }
                    };



                    OrderResponceInfo orderResponceInfo = new OrderResponceInfo()
                    {
                        Id = order.Id,
                        UserId = order.UserId,
                        categories = categoryResponceInfos,
                        PartialBalance = order.PartialBalance,
                        CostDilivary = order.CostDilivary,
                        TotalBalance = order.TotalBalance,
                        OrderStatus = order.Status.ToString(),
                        PhoneNumber = order.PhoneNumberOfUser,


                    };
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.Result = orderResponceInfo;

                }
                else
                {
                    operation.OperrationResultType = OperationResultType.NotExit;
                    operation.Message = " Ypur order not found";
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
        public async Task<OperationResult<bool>> DeletOrder (int id) 
        {
            OperationResult<bool> operation = new OperationResult<bool>();
            try
            {
               
                var order = _context.Orders.FirstOrDefault(o => o.Id == id);
                if (order == null || order.IsDeleted)
                {
                    operation.OperrationResultType = OperationResultType.NotExit;
                    operation.Result = false;
                }
                order.IsDeleted = true;
                order.DatetimeDeleted = DateTime.Now;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = true;
            }
            catch(Exception ex)
            {
                operation.OperrationResultType= OperationResultType.Exception;  
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<dynamic>> GetOrderById(int id) {
            OperationResult<dynamic> operation = new OperationResult<dynamic>();
            try
            {
                var result = await _context.Orders.Where(d => !d.IsDeleted && d.Id==id).Include(c => c.Categories)
                  .Select(o => new {
                      Id= o.Id,
                      PhoneNumberOfUser=  o.PhoneNumberOfUser,
                      UserId=o.UserId,
                      Quantity = o.Categories.Select(q => q.Quantity),
                      PartialBalance= o.PartialBalance,
                      TotalBalance= o.TotalBalance,
                  })
                .ToListAsync();

                if (result.Count > 0)
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
    
        public async Task<OperationResult<dynamic>> GetAllOreders()
        {
            OperationResult<dynamic> operation = new OperationResult<dynamic>();
            try
            {
                var result = await _context.Orders.Where(d => !d.IsDeleted).Include(c=>c.Categories)
                  .Select(o => new {o.Id, o.PhoneNumberOfUser,o.UserId ,
                      Quantity = o.Categories.Select(q=>q.Quantity),
                      o.PartialBalance, o.TotalBalance, })
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
    }
}

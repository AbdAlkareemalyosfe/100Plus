using Base.Models;
using Base;
using IRepostry.IRepo;
using IRepostry.Model_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Twilio.TwiML.Voice;
using Shared_Core;
using IRepostry.Model_Respons;

namespace Repostry.Repo
{
    public class RepoOffer :  IRepoOffer
    {
        private readonly HundredPlusDbContext _context;
        public RepoOffer(HundredPlusDbContext context) 
        {
            _context = context;
        }
        public async Task<OperationResult<OfferResponceInfo>> AddOffer(OfferDtoModel offerDto)
        {
            OperationResult<OfferResponceInfo> operation = new OperationResult<OfferResponceInfo>();
            try
            {
                if (offerDto.ProductId == null)
                {
                    throw new ArgumentException("ProductId cannot be null");
                }
                Offer offer = new Offer()
                {
                    Name = offerDto.Name,
                    Description = offerDto.Description,
                    StartOffer = offerDto.StartOffer,
                    EndOffer = offerDto.EndOffer,
                    NewPrice = offerDto.Newprice,
                    ProductId = (int)offerDto.ProductId,

                };
              
                await _context.Offers.AddAsync(offer);
                await _context.SaveChangesAsync();
                OfferResponceInfo offerResponceInfo = new OfferResponceInfo()
                {
                    Id= offer.Id,
                    Name = offerDto.Name,
                    Description = offerDto.Description,
                    StartOffer = offerDto.StartOffer,
                    EndOffer = offerDto.EndOffer,
                    NewPrice = offerDto.Newprice

                };
                
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = offerResponceInfo;
            }
            catch (Exception ex) 
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<OfferResponceInfo>> UpdateOffer(OfferDtoUp offerDto)
        {
            OperationResult<OfferResponceInfo> operation = new OperationResult<OfferResponceInfo>();
            try
            {
                var offer = await _context.Offers.Where(p => p.Id == offerDto.Id).SingleOrDefaultAsync();
                if (offer == null)
                {
                    operation.OperrationResultType = OperationResultType.Failed;
                    operation.Result = null;
                }

                if (!string.IsNullOrEmpty(offerDto.Name))
                {
                    offer.Name = offerDto.Name;
                }

                if (!string.IsNullOrEmpty(offerDto.Description))
                {
                    offer.Description = offerDto.Description;
                }

                if (offerDto.StartOffer != null)
                {
                    offer.StartOffer = offerDto.StartOffer;
                }

                if (offerDto.EndOffer != null)
                {
                    offer.EndOffer = offerDto.EndOffer;
                }

                if (offerDto.ProductId != 0)
                {
                    offer.ProductId = offerDto.ProductId;
                }

                if (offerDto.Newprice != 0.0)
                {
                    offer.NewPrice = offerDto.Newprice;
                }
                 
                _context.Offers.Update(offer);
                await _context.SaveChangesAsync();
                OfferResponceInfo offerResponceInfo = new OfferResponceInfo() 
                {
                    Id = offer.Id,
                    Name = offer.Name,
                    Description = offer.Description,
                    StartOffer = offer.StartOffer,
                    EndOffer = offer.EndOffer,
                    NewPrice = offer.NewPrice
                };

                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = offerResponceInfo;
            }
            catch(Exception ex) 
            {
                operation.OperrationResultType=OperationResultType.Exception;
                operation.Exception = ex;
            }

            return operation;
        }
        public OperationResult<bool> DeletOffer(int Id)
        {
            OperationResult<bool> operation = new OperationResult<bool>();
            try
            {
                var offer = _context.Offers.Where(p => p.Id == Id && !p.IsDeleted).SingleOrDefault();
                if (offer == null)
                { 
                    operation.OperrationResultType = OperationResultType.NotExit;
                    operation.Result = false;
                    return operation;
                }
                else if (offer.IsDeleted)
                {
                    operation.OperrationResultType = OperationResultType.previouslyDeleted;
                    operation.Result = false;
                }
                else
                {
                    Offer newOffer = new Offer()
                    {
                        Id = Id,
                        IsDeleted = true,
                        DatetimeDeleted = DateTime.Now,
                    };
                    _context.Offers.Update(newOffer);
                    _context.SaveChanges();
                    operation.OperrationResultType = OperationResultType.Success;
                    operation.Result = true;
                }
                
            }
            catch (Exception ex)
            {
                operation.OperrationResultType= OperationResultType.Exception;  
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<dynamic>> GetAllOffers()
        {
            OperationResult<dynamic> operation = new OperationResult<dynamic>();
            try
            {
                var result = await _context.Offers.Where(d => !d.IsDeleted).Include(p=>p.product)
                    .Select(o => new {o.Id , o.Name ,o.StartOffer , o.EndOffer,o.product.Price,o.NewPrice,o.Description})
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
        public async Task<OperationResult<dynamic>> GetOfferById(int id )
        {
            OperationResult<dynamic> operation = new OperationResult<dynamic>();
            try
            {
                var result = await _context.Offers.Where(d => !d.IsDeleted && d.Id ==id).Include(p => p.product)
                     .Select(o => new { o.Id, o.Name, o.StartOffer, o.EndOffer, o.product.Price, o.NewPrice, o.Description })
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

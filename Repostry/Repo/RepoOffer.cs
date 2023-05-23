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

namespace Repostry.Repo
{
    public class RepoOffer : BaseRepostry<Offer>, IRepoOffer
    {
        private readonly HundredPlusDbContext _context;
        public RepoOffer(HundredPlusDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<OperationResult<Offer>> AddOffer(OfferDtoModel offerDto)
        {
            OperationResult<Offer> operation = new OperationResult<Offer>();
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
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = offer;
            }
            catch (Exception ex) 
            {
                operation.OperrationResultType = OperationResultType.Exception;
                operation.Exception = ex;
            }
            return operation;
        }
        public async Task<OperationResult<Offer>> UpdateOffer(OfferDtoUp offerDto)
        {
            OperationResult<Offer> operation = new OperationResult<Offer>();
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
                operation.OperrationResultType = OperationResultType.Success;
                operation.Result = offer;
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
                var offer = _context.Offers.Where(p => p.Id == Id).SingleOrDefault();
                if (offer != null)
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
                else
                {
                    operation.OperrationResultType = OperationResultType.Failed;
                    operation.Result = false;
                }
            }
            catch (Exception ex)
            {
                operation.OperrationResultType= OperationResultType.Exception;  
                operation.Exception = ex;
            }
            return operation;
        }
    }
}

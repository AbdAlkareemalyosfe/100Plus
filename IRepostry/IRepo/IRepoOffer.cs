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
    public interface IRepoOffer : IBaseRepostry<Offer>
    {
        Task<OperationResult<Offer>> AddOffer(OfferDtoModel offerDto);
        Task<OperationResult<Offer>> UpdateOffer(OfferDtoUp offerDto);
        public OperationResult<bool> DeletOffer(int Id);
    }
}

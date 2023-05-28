using Base.Models;
using IRepostry.Model_Dto;
using IRepostry.Model_Respons;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.IRepo
{
    public interface IRepoOffer 
    {
        Task<OperationResult<OfferResponceInfo>> AddOffer(OfferDtoModel offerDto);
        Task<OperationResult<OfferResponceInfo>> UpdateOffer(OfferDtoUp offerDto);
        public OperationResult<bool> DeletOffer(int Id);
        Task<OperationResult<dynamic>> GetAllOffers();

        Task<OperationResult<dynamic>> GetOfferById(int id);


    }
}

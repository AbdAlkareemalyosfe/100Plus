using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Dto
{
    public class CategoryDtoModel
    {
        //[Required ]
        //[DisplayName("Id of your category")]
        //public int Id { get; set; }

       // [ProductIdOrOfferIdRequired(ErrorMessage = "Either ProductId or OfferId must be set, but not both.")]
        public int? ProductId { get; set; }

        //[ProductIdOrOfferIdRequired(ErrorMessage = "Either ProductId or OfferId must be set, but not both.")]
        public int? OfferId { get; set; }

        public int Quantity { get; set; }


    }


    //public class ProductIdOrOfferIdRequired : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var dto = (CategoryDtoModel)validationContext.ObjectInstance;

    //        if (dto.ProductId.HasValue && dto.OfferId.HasValue)
    //        {
    //            return new ValidationResult("Either ProductId or OfferId must be set, but not both.");
    //        }

    //        if (!dto.ProductId.HasValue && !dto.OfferId.HasValue)
    //        {
    //            return new ValidationResult("Either ProductId or OfferId must be set.");
    //        } 

    //        if ((dto.ProductId.HasValue && dto.ProductId.Value == 0 && !dto.OfferId.HasValue) ||
    //            (dto.OfferId.HasValue && dto.OfferId.Value == 0 && !dto.ProductId.HasValue))
    //        {
    //            return new ValidationResult("Either ProductId or OfferId must be set, but not both can be 0.");
    //        }

    //        return ValidationResult.Success;
    //    }
    //}
    //public class ProductIdOrOfferIdRequired : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var dto = (CategoryDtoModel)validationContext.ObjectInstance;

    //        if ((dto.ProductId.Equals(null) || dto.ProductId.Equals(0)) && (dto.OfferId.Equals(null) || dto.OfferId.Equals(0)))
    //        {
    //            return new ValidationResult(ErrorMessage);
    //        }

    //        if (!(dto.ProductId.Equals(null) || dto.ProductId.Equals(0)) && !(dto.OfferId.Equals(null) || dto.OfferId.Equals(0)))
    //        {
    //            return new ValidationResult(ErrorMessage);
    //        }

    //        return ValidationResult.Success;
    //    }
}



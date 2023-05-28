using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Dto
{
    public class OfferDtoUp
    {
        [Required(ErrorMessage = "The Id of offer is required")]
        [DisplayName("Id of Offer")]
        public int Id { get; set; }

        [DisplayName("Name  of product for Offer")]
        public string Name { get; set; }

        [DisplayName("Description  for Offer ")]
        public string? Description { get; set; }
        public DateTime? StartOffer { get; set; }

        [DisplayName("The time  for end Offer")]
        public DateTime? EndOffer { get; set; }

        [DisplayName("Id of product for Offer")]
        public int ProductId { get; set; }
      

        [DisplayName("New Price of product for Offer")]
        public double Newprice { get; set; }
    }
}

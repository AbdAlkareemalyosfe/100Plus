using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Dto
{
    public class OfferDtoModel
    {
        [Required(ErrorMessage ="The name of offer is required")]
        [DisplayName("Name of Offer")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartOffer { get; set; }

        [DisplayName("The time  for end Offer")]
        public DateTime? EndOffer { get; set; }

        [Required(ErrorMessage ="The Id of product is reqiured")]
        [DisplayName("Id of product for Offer" )]
        public int? ProductId { get; set; }

        //[Required(ErrorMessage = "The Id of product is reqiured")]
        //[DisplayName("Id of Category for Offer")]
        //public int CategoryId { get; set; }
       

        [Required(ErrorMessage ="The new Price of product is required")]
        [DisplayName("New Price of product for Offer")]
        public double Newprice { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Dto
{
    public class CategoryDtoUpdate
    {
        [Required(ErrorMessage = "The Id of category is required")]
        [DisplayName("Id of category")]
        public int Id { get; set; }

        [DisplayName("Id of Product If no enter 0")]
        public int ProductId { get; set; }

        [DisplayName("  Id Of Offer If no enter 0")]
        public int OfferId { get; set; }

        [DisplayName("Quantity Of product")]
        public int Quantity { get; set; }
   

    }
}

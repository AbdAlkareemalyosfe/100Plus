using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Dto
{
    public class ProductDtoModel
    {
        [Required(ErrorMessage = "The Name field is required")]
        [DisplayName("Name of Product")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "The Price field must be greater than 0")]
        [Required(ErrorMessage = "The Price field is required")]
        [DisplayName("Price of Product")]
        public double Price { get; set; }
    }
}

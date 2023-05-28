using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Dto
{
    public class OrderdtoUpdate
    {
        [Required(ErrorMessage = "The Id of Order is required")]
        [DisplayName("Id of Order")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> CategoryIds { get; set; } 

    }
}

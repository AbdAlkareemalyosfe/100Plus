using Shared_Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class Order : BaseEntity 
    {
        public string PhoneNumberOfUser { get; set; }
        
        public long PartialBalance {get; set; }
        public double CostDilivary {get; set; }
        public long TotalBalance {get; set; }
       
        public ICollection<Category> Categories { get; set; }
        public User user { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }

    }
}

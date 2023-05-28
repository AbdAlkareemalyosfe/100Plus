using Shared_Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class Category : BaseEntity
    {
        public int Quantity { get; set; }
        public double CostCategory { get; set; }
        public Product Product { get; set;}
       // [ForeignKey("ProductId")]
        public int? ProductId { get; set; }
        public Offer Offer { get; set; }
        //[ForeignKey("OfferId")]
        public int? OfferId { get; set; }
        public Order Order { get; set; }
        [ForeignKey("OrderId")]
        public int? OrderId { get; set; }
    }
}

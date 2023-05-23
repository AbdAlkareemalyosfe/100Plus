using Shared_Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class Offer : BaseEntity 
    {
       
        public string Name { get; set; }
        public string Description { get; set; } 
        public string  StartOffer { get; set; }
        public string EndOffer { get; set; }
        public double NewPrice { get; set; }
         public Product? product { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        public ICollection<Category>? Categories { get; set; }
     
       // public int CategoryId { get; set; }
       




    }
}

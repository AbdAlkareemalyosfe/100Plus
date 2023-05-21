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
        public DateTime StartOffer { get; set; }
        public DateTime EndOffer { get; set; }
        public string content { get; set; }
        public double PrevisonPrice {get ; set; }
        public double Newprice { get; set; }
         public Product product { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
     
        public int CategoryId { get; set; }
       




    }
}

using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public class Product : BaseEntity 
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
       
        public ICollection<Category> Categories { get; set; }
        public ICollection<Offer> Offers { get; set; }
       

    }
}

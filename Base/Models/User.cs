using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Models
{
    public  class User : BaseEntity
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
      
        public string Token { get ; set; }  
        public IEnumerable<Order> orders { get; set; }
        public bool IsAdmin { get; set; }
    }
}

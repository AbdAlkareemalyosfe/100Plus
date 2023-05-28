using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Dto
{
    public  class OrderdtoModel
    {
        public int UserId { get; set; }
        public List<int > CategoryIds { get; set; }
    }
}

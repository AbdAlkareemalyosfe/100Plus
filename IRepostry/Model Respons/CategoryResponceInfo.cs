using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Respons
{
    public  class CategoryResponceInfo
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double CostCategory { get; set; }
        public int? ProductId { get; set; }
        public int? OfferId { get; set; }

    }
}

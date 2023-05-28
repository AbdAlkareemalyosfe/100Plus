using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Respons
{
    public class OfferResponceInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartOffer { get; set; }
        public DateTime? EndOffer { get; set; }
        public double NewPrice { get; set; }

    }
}

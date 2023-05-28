using Base.Models;
using Shared_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry.Model_Respons
{
    public class OrderResponceInfo
    {
        public int Id { get; set; } 
        public string PhoneNumber { get; set; }
        public long PartialBalance { get; set; }
        public double CostDilivary { get; set; } 
        public long TotalBalance { get; set; }
        public int UserId { get; set; }
        public ICollection<CategoryResponceInfo> categories { get; set; }
        public  string OrderStatus  { get; set; }
        
    }
}

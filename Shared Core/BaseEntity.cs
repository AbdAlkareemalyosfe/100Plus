using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Core
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTimeOffset? DatetimeDeleted { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}

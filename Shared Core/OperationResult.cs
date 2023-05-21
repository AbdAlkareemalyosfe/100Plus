using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Core
{
    public class OperationResult<T>
    {
        public OperationResultType OperrationResultType { get; set;}
        public T Result { get; set;}
        public IEnumerable<T> RangeResults { get; set;}
        public Exception Exception { get; set;}
        public string Message { get; set;}
    }
}

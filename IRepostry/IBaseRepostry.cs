using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IRepostry
{
    public interface IBaseRepostry<T> where T : class 
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T item);
       
        Task<bool> UpDate(T item);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);




    }
   
}

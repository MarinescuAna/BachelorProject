using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamWork.ApplicationLogic.Repository.Models.Interface
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetItems();
        Task<T> GetItem(Expression<Func<T,Boolean>> expression);
        void InsertItem(T item);
        Task<Boolean> UpdateItem(Expression<Func<T, Boolean>> expression,T item);
        Task<Boolean> DeleteItem(Expression<Func<T, Boolean>> expression);

    }
}

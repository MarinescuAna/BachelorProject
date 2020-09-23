using AplicationLogic.Repository.Models.Interface;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Repository.Models.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        protected TeamWorkDbContext context;
        public BaseRepository(TeamWorkDbContext ctx)
        {
            context = ctx;
        }
        public async Task<bool> DeleteItem(Expression<Func<T, bool>> expression)
        {
            T itemFind = await GetItem(expression);

            if(itemFind==null)
            {
                return false;
            }

            context.Set<T>().Remove(itemFind);

            return true;
        }

        public async Task<T> GetItem(Expression<Func<T, bool>> expression)
        {
            return await context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetItems()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async void InsertItem(T item)
        {
            await context.Set<T>().AddAsync(item);
        }

        public async Task<bool> UpdateItem(Expression<Func<T, bool>> expression, T item)
        {
            T itemFind = await GetItem(expression);

            if (itemFind == null)
            {
                return false;
            }

            itemFind = item;

            context.Set<T>().Update(itemFind);

            return true;
        }
    }
}

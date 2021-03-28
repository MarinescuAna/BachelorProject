
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.ApplicationLogic.Repository.Utils;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected TeamWorkDbContext context;
        private readonly ILoggerService applicationLogger;
        public BaseRepository(TeamWorkDbContext ctx, ILoggerService logger)
        {
            context = ctx;
            applicationLogger = logger;
        }
        public async Task<bool> DeleteItem(Expression<Func<T, bool>> expression)
        {
            T itemFind = await GetItem(expression);

            if (itemFind == null)
            {
                return false;
            }

            try
            {
                context.Set<T>().Remove(itemFind);

                return true;
            }
            catch(Exception ex)
            {
                applicationLogger.LogError($"{Messages.Delete} {itemFind.GetType().Name}", ex.Message);

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError($"{Messages.Delete} {itemFind.GetType().Name}", ex.InnerException.Message);
                }
            }

            return false;
        }

        public async Task<T> GetItem(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                applicationLogger.LogError("GetItem", ex.Message);

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError("GetItem", ex.InnerException.Message);
                }

                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> GetItems()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async void InsertItem(T item)
        {   
            try
            {
                await context.Set<T>().AddAsync(item);
            }
            catch (Exception ex)
            {
                applicationLogger.LogError($"{Messages.Delete} {item.GetType().Name}", ex.Message);

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError($"{Messages.Delete} {item.GetType().Name}", ex.InnerException.Message);
                }
            }

        }

        public async Task<bool> UpdateItem(T item)
        {
            try
            {
                context.Set<T>().Update(item);

                return true;
            }
            catch (Exception ex)
            {
                applicationLogger.LogError($"{Messages.Delete} {item.GetType().Name}", ex.Message);

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError($"{Messages.Delete} {item.GetType().Name}", ex.InnerException.Message);
                }
            }
            return false;
        }
    }
}

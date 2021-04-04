
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
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
            applicationLogger.LogInfo("Try to retrieve form database the record which meets the condition.");
            T itemFind = await GetItem(expression);

            if (itemFind == null)
            {
                applicationLogger.LogInfo("No data was find");
                return false;
            }
            try
            {
                applicationLogger.LogInfo("The record was found and now it try to remove the item");
                context.Set<T>().Remove(itemFind);

                return true;
            }
            catch (Exception ex)
            {
                applicationLogger.LogError($"The item can't be remove from some reasons. The follow error appear: {ex.Message}");

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError($"Inner Exception Message: {ex.InnerException.Message}");
                }
            }

            return false;
        }

        public async Task<T> GetItem(Expression<Func<T, bool>> expression)
        {
            try
            {
                applicationLogger.LogInfo("Try to retrieve form database the record which meets the condition.");
                return await context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                applicationLogger.LogError($"The item can't be retreived from some reasons. The follow error appear: {ex.Message}");

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError($"Inner Exception Message: {ex.InnerException.Message}");
                }
            }

            return null;
        }

        public virtual async Task<IEnumerable<T>> GetItems()
        {
            try
            {
                applicationLogger.LogInfo("Try to retrieve form database all records.");
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                applicationLogger.LogError($"The items can't be retrieved from some reasons. The follow error appear: {ex.Message}");

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError($"Inner Exception Message: {ex.InnerException.Message}");
                }
                return null;
            }
        }

        public async void InsertItem(T item)
        {
            try
            {
                applicationLogger.LogInfo("Try to insert the record into database.");
                await context.Set<T>().AddAsync(item);
            }
            catch (Exception ex)
            {
                applicationLogger.LogError($"The item can't be inserted from some reasons. The follow error appear: {ex.Message}");

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError($"Inner Exception Message: {ex.InnerException.Message}");
                }
            }
        }

        public async Task<bool> UpdateItem(T item)
        {
            try
            {
                applicationLogger.LogInfo("Try to update the record into database.");
                context.Set<T>().Update(item);

                return true;
            }
            catch (Exception ex)
            {
                applicationLogger.LogError($"The item can't be updated from some reasons. The follow error appear: {ex.Message}");

                if (ex.InnerException != null)
                {
                    applicationLogger.LogError($"Inner Exception Message: {ex.InnerException.Message}");
                }
            }
            return false;
        }
    }
}

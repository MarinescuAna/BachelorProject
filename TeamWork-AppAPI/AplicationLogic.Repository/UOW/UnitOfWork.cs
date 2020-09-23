using AplicationLogic.Repository.Models.Implementation;
using AplicationLogic.Repository.Models.Interface;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly TeamWorkDbContext context;
        private IUserRepository _User;
        public UnitOfWork(TeamWorkDbContext ctx)
        {
            context = ctx;
        }

        public IUserRepository User {
            get
            {
                if(_User==null)
                {
                    _User = new UserRepositoryImpl(context);
                }

                return _User;
            }
        }

        public async Task<int> Commit()
        {
           return await context.SaveChangesAsync();
        }

        public async void Dispose()
        {
             await context.DisposeAsync();
        }
    }
}

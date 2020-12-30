using TeamWork.DataAccess.Repository;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork.ApplicationLogger;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class UserRepositoryImpl :BaseRepository<User>, IUserRepository
    {
        public UserRepositoryImpl(TeamWorkDbContext ctx, ILoggerService loggerService):base(ctx, loggerService)
        {
           
        }
       
    }
}

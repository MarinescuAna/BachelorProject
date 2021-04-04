using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class AssignmentRepository: BaseRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(TeamWorkDbContext teamWorkDbContext, ILoggerService loggerService) : base(teamWorkDbContext, loggerService)
        {

        }
    }
}

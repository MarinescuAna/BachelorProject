using System;
using System.Collections.Generic;
using System.Text;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Implementation;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class AverageRepositoryImpl:BaseRepository<Average>,IAverageRepository
    {
        public AverageRepositoryImpl(TeamWorkDbContext teamWorkDbContext,ILoggerService loggerService):
            base(teamWorkDbContext,loggerService)
        {

        }
    }
}

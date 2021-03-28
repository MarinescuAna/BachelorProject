using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models.Domain;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class ImageRepositoryImpl:BaseRepository<Image>,IImageRepository
    {
        public ImageRepositoryImpl(TeamWorkDbContext teamWorkDbContext,ILoggerService loggerService):base(teamWorkDbContext,loggerService)
        {

        }

        public override async Task<IEnumerable<Image>> GetItems() => await context.Images
               .Include(s => s.User)
               .ToListAsync();
    }
}

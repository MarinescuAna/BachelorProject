using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogger;
using TeamWork.ApplicationLogic.Repository.Models.Interface;
using TeamWork.DataAccess.Domain.Models;
using TeamWork.DataAccess.Repository;

namespace TeamWork.ApplicationLogic.Repository.Models.Implementation
{
    public class ImageRepositoryImpl:BaseRepository<Image>,IImageRepository
    {
        public ImageRepositoryImpl(TeamWorkDbContext teamWorkDbContext,ILoggerService loggerService):
            base(teamWorkDbContext,loggerService)
        {

        }

        public override async Task<IEnumerable<Image>> GetItems() =>
            await context.Images
               .Include(s => s.User)
               .ToListAsync();
        public override async Task<Image> GetItem(Expression<Func<Image, bool>> expression) =>
           await context.Images
           .Include(s => s.User)
           .AsNoTracking()
           .FirstOrDefaultAsync(expression);
    }
}

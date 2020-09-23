using AplicationLogic.Repository.Models.Interface;
using DataAccess.Domain.Models.Domain;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AplicationLogic.Repository.Models.Implementation
{
    public class UserRepositoryImpl :BaseRepository<User>, IUserRepository
    {
        public UserRepositoryImpl(TeamWorkDbContext ctx):base(ctx)
        {
           
        }
       
    }
}

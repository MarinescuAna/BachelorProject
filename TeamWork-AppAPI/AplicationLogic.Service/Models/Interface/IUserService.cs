using DataAccess.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Service.Models.Interface
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string userEmail);
        Task<int> InsertUser(User user);
        Task<int> UpdateUserInformation(User user);
    }
}

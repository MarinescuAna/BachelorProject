
using AplicationLogic.Repository.Models.Implementation;
using AplicationLogic.Repository.Models.Interface;
using AplicationLogic.Repository.UOW;
using AplicationLogic.Service.Models.Interface;
using DataAccess.Domain.Models.Domain;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Service.Models.Implementation
{
    public class UserServiceImpl :ABaseService, IUserService
    {   
        public UserServiceImpl(IUnitOfWork uow):base(uow)
        {}
        public Task<User> GetUserByEmailAsync(string userEmail)
        {
           return _unitOfWork.User.GetItem(u=>u.EmailAddress==userEmail);    
        }

        public Task<int> InsertUser(User user)
        {
            _unitOfWork.User.InsertItem(user);

            return _unitOfWork.Commit("UserServiceImpl -> InsertUser");
        }

        public Task<int> UpdateUserInformation(User user)
        {
            _unitOfWork.User.UpdateItem(u => u.UserId == user.UserId, user);

            return _unitOfWork.Commit("UserServiceImpl -> UpdateUserInformation");
        }
    }
}

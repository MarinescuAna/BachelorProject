﻿using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Models.Domain;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class UserServiceImpl :ABaseService, IUserService
    {   
        public UserServiceImpl(IUnitOfWork uow):base(uow)
        {}
        public Task<User> GetUserByEmailAsync(string userEmail) => _unitOfWork.User.GetItem(u => u.EmailAddress == userEmail);

        public Task<int> InsertUserAsync(User user)
        {
            _unitOfWork.User.InsertItem(user);

            return _unitOfWork.Commit("UserServiceImpl -> InsertUser");
        }

        public Task<int> UpdateUserInformationAsync(User user)
        {
            _unitOfWork.User.UpdateItem(u => u.UserId == user.UserId, user);

            return _unitOfWork.Commit("UserServiceImpl -> UpdateUserInformation");
        }
    }
}

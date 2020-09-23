
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
    public class UserServiceImpl : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserServiceImpl(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        public Task<User> GetUserByEmail(string userEmail)
        {
           return _unitOfWork.User.GetItem(u=>u.EmailAddress==userEmail);    
        }

        public Task<int> InsertUser(User user)
        {
            _unitOfWork.User.InsertItem(user);

            return _unitOfWork.Commit();
        }

        public Task<int> UpdateUserInformation(User user)
        {
            _unitOfWork.User.UpdateItem(u => u.UserId == user.UserId, user);

            return _unitOfWork.Commit();
        }
    }
}

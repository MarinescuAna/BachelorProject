using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.Common.ConstantNumbers;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class ChatServiceImpl:ABaseService, IChatService
    {
        public ChatServiceImpl(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public async Task<List<Message>> GetMessagesByGroupKeyAsync(string groupKey) => 
            (await _unitOfWork.Message.GetItems())
            .Where(u=>u.GroupID.ToString()==groupKey)
            .OrderBy(u=>u.DateSent)
            .ToList();
        public async Task<Message> GetMessageByKeyAsync(string key) =>
            await _unitOfWork.Message.GetItem(u => u.ID.ToString() == key);
        public async Task<bool> SaveMessageByGroupKeyAsync(Message message)
        {
            _unitOfWork.Message.InsertItem(message);

            return await _unitOfWork.Commit() > Number.Number_0;            
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            await _unitOfWork.Message.UpdateItem(message);

            return (await _unitOfWork.Commit()) > Number.Number_0;
        }

        public async Task<bool> DeleteMessageAsync(string messageKey)
        {
            await _unitOfWork.Message.DeleteItem(u=>u.ID.ToString()==messageKey);

            return (await _unitOfWork.Commit()) > Number.Number_0;
        }
    }
}

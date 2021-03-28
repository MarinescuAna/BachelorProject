using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.ApplicationLogic.Service.Utils;
using TeamWork.DataAccess.Domain.Models.Domain;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class ChatServiceImpl:ABaseService, IChatService
    {
        public ChatServiceImpl(IUnitOfWork unitOfWork):base(unitOfWork)
        {
        }

        public async Task<Chat> GetChatByGroupKeyAsync(string groupKey) => await _unitOfWork.Chat.GetItem(
            s => s.GroupUniqueID.ToString() == groupKey);
        public async Task<List<Message>> GetMessagesByChatKeyAsync(string chatKey) => (await _unitOfWork.Message.GetItems())
            .Where(u=>u.ChatID.ToString()==chatKey)
            .OrderBy(u=>u.DateSent)
            .ToList();
        public async Task<Message> GetMessageByKeyAsync(string key) => await _unitOfWork.Message.GetItem(u => u.ID.ToString() == key);
        public async Task<bool> SaveMessageByGroupKeyAsync(string groupKey, Message message)
        {
            var chatFound = await GetChatByGroupKeyAsync(groupKey);

            if (chatFound == null)
            {
                var newId = Guid.NewGuid();
                message.Chat=new Chat
                {
                    ChatID = newId,
                    GroupUniqueID = Guid.Parse(groupKey)
                };
                message.ChatID = newId;
            }
            else
            {
                message.ChatID = chatFound.ChatID;
            }

            _unitOfWork.Message.InsertItem(message);

            return (await _unitOfWork.Commit(Messages.SaveMessageByGroupKeyAsync)) > 0;            
        }

        public async Task<bool> UpdateMessageAsync(Message message)
        {
            await _unitOfWork.Message.UpdateItem(message);

            return (await _unitOfWork.Commit(Messages.UpdateMessageAsync)) > 0;
        }

        public async Task<bool> DeleteMessageAsync(string messageKey)
        {
            await _unitOfWork.Message.DeleteItem(u=>u.ID.ToString()==messageKey);

            return (await _unitOfWork.Commit(Messages.DeleteMessageAsync)) > 0;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IChatService
    {
        Task<bool> DeleteMessageAsync(string messageKey);
        Task<bool> UpdateMessageAsync(Message message);
        Task<Message> GetMessageByKeyAsync(string key);
        Task<bool> SaveMessageByGroupKeyAsync( Message message);
        Task<List<Message>> GetMessagesByGroupKeyAsync(string groupKey);
    }

}

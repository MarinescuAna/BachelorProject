using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IListService
    {
        Task<bool> DeleteListAsync(Guid guid);
        Task<bool> InsertListAsync(List list);
        Task<List<List>> GetListsByEmailAsync(string email);
        Task<List<List>> GetListsByGroupIdAsync(Guid groupId);
        Task<bool> UpdateListAsync(List list);
        Task<List> GetListByListIdAsync(Guid listId);
    }
}

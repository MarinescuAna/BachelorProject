using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IListService
    {
        Task<bool> InsertListAsync(List list);
        Task<List<List>> GetListsAsync(string email);
        Task<DateTime> GetListOverroleDeadline(Guid listId);
    }
}

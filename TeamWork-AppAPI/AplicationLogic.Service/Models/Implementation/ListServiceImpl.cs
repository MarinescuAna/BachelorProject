using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class ListServiceImpl: ABaseService, IListService
    {
        public ListServiceImpl(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
        public async Task<bool> DeleteListAsync(Guid guid)
        {
            await _unitOfWork.List.DeleteItem(u=>u.ListID==guid);

            return await _unitOfWork.Commit() > 0;
        }
        public async Task<bool> InsertListAsync(List list)
        {
            _unitOfWork.List.InsertItem(list);

            return await _unitOfWork.Commit() > 0;
        }
        public async Task<bool> UpdateListAsync(List list)
        {
            await _unitOfWork.List.UpdateItem(list);

            return (await _unitOfWork.Commit()) > 0;
        }
        public async Task<List<List>> GetListsByEmailAsync(string email) => 
            (await _unitOfWork.List.GetItems())
                .Where(u => u.UserID == email)
                .ToList();
        public async Task<List<List>> GetListsByGroupIdAsync(string groupId) =>
           (await _unitOfWork.List.GetItems())
               .Where(u => u.GroupID == groupId)
               .ToList();
        public async Task<List> GetListByListIdAsync(Guid listId) =>
         await _unitOfWork.List.GetItem(u=>u.ListID==listId);
        public async Task<string> GetListOverroleDeadline(Guid listId) =>
            (await _unitOfWork.List.GetItem(u => u.ListID == listId)).ListDeadline;
    }
}

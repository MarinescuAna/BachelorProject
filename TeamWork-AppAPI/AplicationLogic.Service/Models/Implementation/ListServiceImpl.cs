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

        public async Task<bool> InsertListAsync(List list)
        {
            _unitOfWork.List.InsertItem(list);

            return await _unitOfWork.Commit() > 0;
        }
    }
}

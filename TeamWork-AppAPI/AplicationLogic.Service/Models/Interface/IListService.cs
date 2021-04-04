using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IListService
    {
        Task<bool> InsertListAsync(List list);
    }
}

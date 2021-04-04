using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string userEmail);
        Task<int> InsertUserAsync(User user);
        Task<int> UpdateUserInformationAsync(User user);
    }
}

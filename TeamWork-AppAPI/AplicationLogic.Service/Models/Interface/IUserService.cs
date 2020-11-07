using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models.Domain;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAsync(string userEmail);
        Task<int> InsertUser(User user);
        Task<int> UpdateUserInformation(User user);
    }
}

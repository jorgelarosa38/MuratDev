using Murat.Models;
using System.Threading.Tasks;

namespace Murat.BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        Task<object> UserFilter(int page, int rows, string userlogin, string name, string estate);

        Task<object> GetUserId(int userid);

        Task<object> UserMnt(User user);
    }
}

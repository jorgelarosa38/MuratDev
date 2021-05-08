using Murat.BusinessLogic.Utilities;
using Murat.Models;
using System.Threading.Tasks;

namespace Murat.BusinessLogic.Interfaces
{
    public interface ISecurityLogic
    {
        Task<Response> ValidarAccesos(Credenciales credenciales);
    }
}

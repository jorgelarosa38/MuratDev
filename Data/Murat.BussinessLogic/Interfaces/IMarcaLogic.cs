using Murat.Models;
using System.Threading.Tasks;

namespace Murat.BusinessLogic.Interfaces
{
    public interface IMarcaLogic
    {
        Task<object> GetMarcas(string sMarca);

        Task<object> UpdMarca(Marca marca);
    }
}

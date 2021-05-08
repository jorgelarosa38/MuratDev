using Murat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.Repository
{
    public interface IMarcaRepository
    {
        Task<List<Marca>> GetMarcas(string sMarca);

        Task<ResponseSql> UpdMarca(Marca marca);
    }
}

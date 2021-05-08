using Murat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.Repository
{
    public interface IPublicadoRepository
    {
        Task<ResponseSql> UpdPublicado(Publicado publicado);

        Task<object> GetPublicadoID(int cod_Operacion, int idDato);

        Task<List<ExtPublicado>> GetPublicado(int cod_Operacion);
    }
}

using Murat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.Repository
{
    public interface ITablaMaestraRepository
    {
        Task<List<TablaMaestra>> GetTablaMaestra(int IDTABLA);

        Task<List<TablaMaestra>> GetListaTabla(int IDTABLA, int IDPADRE, int SDETALLE);

        Task<List<TablaMaestra>> GetListaTablaId(int IDDETALLE);

        Task<ResponseSql> PostTabla(TablaMaestra tablaMaestra);
    }
}

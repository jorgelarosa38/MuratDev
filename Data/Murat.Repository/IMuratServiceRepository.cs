using Murat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.Repository
{
    public interface IMuratServiceRepository
    {
        Task<List<MenuBar>> GetMenuBar(object Tipo, object Id1, object Id2);

        Task<List<Main>> ListSlider(int Tipo, int Id1, int Id2);

        Task<List<PublicadoProductoServ>> ListPublProducto(FiltroProducto filtroProducto);

        Task<List<ProductoIDServ>> ListPublProductoID(int idProducto);

        Task<object> ListFiltros(int tipo);

        Task<ResponseSql> UpdClientes(MuratClientes clientes);

        Task<ResponseSql> UpdPedido(string xml);
    }
}

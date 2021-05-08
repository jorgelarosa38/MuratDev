using Murat.Models;
using System.Threading.Tasks;

namespace Murat.BusinessLogic.Interfaces
{
    public interface IMuratServicesLogic
    {
        Task<object> GetMenuBar(int tipo, int id1, int id2);

        Task<object> ListSlider(int tipo, int id1, int id2);

        Task<object> ListPublProducto(FiltroProducto filtroProducto);

        Task<object> ListPublProductoID(int idProducto);

        Task<object> ListFiltros(int tipo);

        Task<object> UpdClientes(MuratClientes clientes);

        Task<object> UpdPedido(MuratPedidos pedidos);
    }
}

using Murat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.Repository
{
    public interface IProductoRepository
    {
        Task<ResponseSql> UpdProducto(Producto producto);

        Task<List<Producto>> ListarProductos(int cod_Categoria, string sProducto);

        Task<List<Producto>> GetProducto(string codigo);

        Task<List<Precio>> GetPrecio(int idProducto);

        Task<ResponseSql> UpdPrecio(Precio precio);

        Task<List<ImagenProducto>> GetImagen(int idProducto);

        Task<ResponseSql> UpdImagen(ImagenProducto imagenProducto);

        Task<ResponseSql> UpdTag(TagProducto tag);

        Task<List<TagProducto>> GetListaTags(int IdProducto);
    }
}

using Dapper;
using Murat.Models;
using Murat.Repository;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Murat.DataAccess
{
    public class MuratServiceRepository : IMuratServiceRepository
    {
        protected string _connectionString;
        public MuratServiceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<MenuBar>> GetMenuBar(object Tipo, object Id1, object Id2)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TIPO", Tipo);
            parameters.Add("@ID1", Id1);
            parameters.Add("@ID2", Id2);

            using (var connection = new SqlConnection(_connectionString))
            {
                return (await connection.QueryAsync<MenuBar>("[dbo].[SPE_LIST_MENU]", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<object> ListFiltros(int Tipo)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TIPO", Tipo);

            using (var connection = new SqlConnection(_connectionString))
            {
                if (Tipo == 1)
                {
                    return (await connection.QueryAsync<ListMarcas>("[dbo].[SPE_LIST_FILTRO]", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
                if (Tipo == 2)
                {
                    return (await connection.QueryAsync<ListCategorias>("[dbo].[SPE_LIST_FILTRO]", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
                if (Tipo == 3)
                {
                    return (await connection.QueryAsync<ListArrPrecios>("[dbo].[SPE_LIST_FILTRO]", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();
                }
                return null;
            }
        }

        public async Task<List<PublicadoProductoServ>> ListPublProducto(FiltroProducto filtroProducto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TIPO", filtroProducto.Tipo);
            parameters.Add("@STAG", filtroProducto.STag);
            parameters.Add("@IDCATEGORIA", filtroProducto.IdCategoria);
            parameters.Add("@IDMARCA", filtroProducto.IdMarca);
            parameters.Add("@PRECIO_INI", filtroProducto.Precio_Ini);
            parameters.Add("@PRECIO_FIN", filtroProducto.Precio_Fin);

            using (var connection = new SqlConnection(_connectionString))
            {
                return (await connection.QueryAsync<PublicadoProductoServ>("[dbo].[SPE_LIST_PUB_PRODUCTO]", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();
            }
        }

        public async Task<List<ProductoIDServ>> ListPublProductoID(int idProducto)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IDPRODUCTO", idProducto);

            using (var connection = new SqlConnection(_connectionString))
            {
                SqlMapper.GridReader reader = await connection.QueryMultipleAsync("[dbo].[SPE_LIST_PUB_PRODUCTO_ID]", parameters, commandType: CommandType.StoredProcedure);
                List<ProductoIDServ> productos = reader.Read<ProductoIDServ>().ToList();
                List<ColorIDServ> colores = reader.Read<ColorIDServ>().ToList();
                List<ImagenIDServ> imagenes = reader.Read<ImagenIDServ>().ToList();
                List<TallaIDServ> tallas = reader.Read<TallaIDServ>().ToList();
                List<StockIDServ> stock = reader.Read<StockIDServ>().ToList();
                foreach (var item in productos)
                {
                    item.Color = colores;
                    item.Imagen = imagenes;
                    item.Talla = tallas;
                    item.Stock = stock;
                }

                return productos;
            }
        }

        public async Task<List<Main>> ListSlider(int Tipo, int Id1, int Id2)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TIPO", Tipo);
            parameters.Add("@ID1", Id1);
            parameters.Add("@ID2", Id2);

            using (var connection = new SqlConnection(_connectionString))
            {
                List<Main> LstMain = new List<Main>();
                Main main = new Main();
                SqlMapper.GridReader reader = await connection.QueryMultipleAsync("[dbo].[SPE_LIST_SLIDER]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                List<Slider> ListSlider = reader.Read<Slider>().ToList();
                List<Etiqueta> ListEtiqueta = reader.Read<Etiqueta>().ToList();
                List<Configuracion> ListConfig = reader.Read<Configuracion>().ToList();
                List<MainTag> ListTag = reader.Read<MainTag>().ToList();

                if (ListSlider.Count > 0)
                {
                    main.Sliders = ListSlider;
                    main.Etiquetas = ListEtiqueta;
                    main.Configuracion = ListConfig;
                    main.Tag = ListTag;
                    LstMain.Add(main);
                    return LstMain;
                }

                return null;
            }
        }

        public async Task<ResponseSql> UpdClientes(MuratClientes clientes)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@IDCLIENTE", clientes.IdCliente);
            parameters.Add("@SCORREO", clientes.SCorreo);
            parameters.Add("@SNOMBRE", clientes.SNombre);
            parameters.Add("@SAPELLIDO", clientes.SApellido);
            parameters.Add("@SNOMBRE_LARGO", clientes.SNombre_Largo);
            parameters.Add("@SNRO_TELEFONO", clientes.SNro_Telefono);
            parameters.Add("@CONTRASENA", clientes.Contrasena);

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstAsync<ResponseSql>("[dbo].[SPE_UDP_EXT_CLIENTE]", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<ResponseSql> UpdPedido(string xml)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@XmlDocument", xml);

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstAsync<ResponseSql>("[dbo].[SPE_UDP_EXT_PEDIDO]", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}

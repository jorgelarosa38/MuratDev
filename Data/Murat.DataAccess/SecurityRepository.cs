using Dapper;
using Microsoft.Extensions.Configuration;
using Murat.Models;
using Murat.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Murat.DataAccess
{
    public class SecurityRepository : ISecurityRepository
    {
        protected string _connectionString;
        public IConfiguration Configuration { get; }

        public SecurityRepository(string connectionString, IConfiguration _configuration)
        {
            _connectionString = connectionString;
            Configuration = _configuration;
        }

        public async Task<List<DetalleUsuario>> ValidarAccesos(Credenciales credenciales)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@USUARIO", credenciales.usuario);
            parameters.Add("@CONTRASENA", credenciales.contraseña);


            using (var connection = new SqlConnection(_connectionString))
            {
                GridReader reader = await connection.QueryMultipleAsync("[dbo].[SPE_LIST_EXT_LOGIN]", parameters, commandType: System.Data.CommandType.StoredProcedure);
                List<DetalleUsuario> DetalleUsuario = reader.Read<DetalleUsuario>().ToList();
                List<Accesos> Accesos = reader.Read<Accesos>().ToList();
                List<Accesos> Config = reader.Read<Accesos>().ToList();

                if (DetalleUsuario.Count > 0)
                {
                    var maintence_url = Configuration["AppSettings:Maintence"];
                    foreach (var item in DetalleUsuario)
                    {
                        item.Accesos = Accesos;
                        foreach (var opcion in Accesos)
                        {
                            /*opcion.Url = maintence_url + opcion.Url;*/

                            if (opcion.Name.Equals("CONFIGURACION"))
                            {
                                /*foreach(var child in Config)
                                {
                                    child.Url = maintence_url + child.Url;
                                }*/
                                opcion.Children = Config;
                            }
                        }
                    }
                }
                return DetalleUsuario;
            }
        }
    }
}

using Murat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.Repository
{
    public interface ISecurityRepository
    {
        Task<List<DetalleUsuario>> ValidarAccesos(Credenciales credenciales);
    }
}

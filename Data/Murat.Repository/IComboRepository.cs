using Murat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.Repository
{
    public interface IComboRepository
    {
        Task<List<Combo>> ListarCombo(int TIPO, string PARM1, string PARM2, string PARM3, string PARM4, int VALOR);
    }
}

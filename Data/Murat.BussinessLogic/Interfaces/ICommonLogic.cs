using System.Threading.Tasks;

namespace Murat.BusinessLogic.Interfaces
{
    public interface ICommonLogic
    {
        Task<object> GetNroImagen(int tipo, int id1, int id2, int id3);
    }
}

using Murat.Models;
using System.Threading.Tasks;

namespace Murat.BusinessLogic.Interfaces
{
    public interface ISliderLogic
    {
        Task<object> GetSliders(string cod_Tipo);

        Task<object> UpdSlider(Slider slider);
    }
}

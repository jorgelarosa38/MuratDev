using Murat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.Repository
{
    public interface ISliderRepository
    {
        Task<List<Slider>> GetSliders(string cod_Tipo);

        Task<ResponseSql> UpdSlider(Slider slider);
    }
}

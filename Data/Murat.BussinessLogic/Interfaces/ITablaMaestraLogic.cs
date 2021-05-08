﻿using Murat.Models;
using System.Threading.Tasks;

namespace Murat.BusinessLogic.Interfaces
{
    public interface ITablaMaestraLogic
    {
        Task<object> GetTablaMaestra(int IDTABLA);

        Task<object> PostTabla(TablaMaestra tablaMaestra);

        Task<object> GetListaTabla(int IDTABLA, int IDPADRE, int SDETALLE);

        Task<object> GetListaTablaId(int IDDETALLE);
    }
}

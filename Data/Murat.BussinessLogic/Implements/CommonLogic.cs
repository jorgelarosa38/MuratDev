using Murat.BusinessLogic.Interfaces;
using Murat.BusinessLogic.Utilities;
using Murat.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Murat.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace Murat.BusinessLogic.Implementations
{
    public class CommonLogic : ICommonLogic
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public CommonLogic(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<object> GetNroImagen(int tipo, int id1, int id2, int id3)
        {
            Response response = new Response();
            try
            {
                List<ListNroImagen> list = await _unitOfWork.Common.GetNroImagen(tipo, id1, id2, id3);

                if (list.Count > 0)
                {
                    string directory = _config.GetSection("AppSettings").GetSection("url_imagenes").Value;
                    foreach (var imagen in list)
                    {
                        foreach (var item in imagen.Archivo_Imagenes)
                        {
                            var categoria = "Producto";
                            if (tipo == 2)
                                categoria = "Slider";

                            item.Url_Imagen = AuxiliarMethods.GenerarURL(directory, categoria, item.SArchivo);
                        }
                    }

                    response.Status = Constant.Status;
                    response.Message = Constant.Ok;
                    response.Data = list;
                }
                else
                {
                    response.Status = Constant.Error400;
                    response.Message = Constant.Consult;
                }
            }
            catch (Exception e)
            {
                response.Status = Constant.Error500;
                response.Message = e.Message;
            }
            return response;
        }
    }
}

﻿using Murat.BusinessLogic.Interfaces;
using Murat.BusinessLogic.Utilities;
using Murat.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Murat.UnitOfWork;
using Microsoft.Extensions.Configuration;

namespace Murat.BusinessLogic.Implementations
{
    public class MarcaLogic : IMarcaLogic
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public MarcaLogic(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<object> GetMarcas(string sMarca)
        {
            Response response = new Response();
            try
            {
                List<Marca> list = await _unitOfWork.Marca.GetMarcas(sMarca);

                if (list.Count > 0)
                {
                    string directory = _config.GetSection("AppSettings").GetSection("url_imagenes").Value;
                    foreach (var item in list)
                    {
                        if (item.SArchivo != "")
                        {
                            string url_imagen = AuxiliarMethods.GenerarURL(directory,"Marca", item.SArchivo);
                            item.UrlImagen = url_imagen;
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

        public async Task<object> UpdMarca(Marca marca)
        {
            Response response = new Response();
            ResponseSql responsesql = new ResponseSql();

            try
            {
                if (marca.ACCION == "A" || marca.ACCION == "M")
                {
                    string directory = _config.GetSection("AppSettings").GetSection("directory").Value;
                    marca.SArchivo = marca.SArchivo.ToString().Trim();
                    marca.BImagen = marca.BImagen.ToString().Trim();
                    if (marca.SArchivo != "" && marca.BImagen != "")
                    {
                        AuxiliarMethods.Base64ToImage(directory, marca.BImagen, marca.SArchivo, "Marca");
                    }
                }
                responsesql = await _unitOfWork.Marca.UpdMarca(marca);
                response.Status = responsesql.ID_ERR == 0 ? Constant.Status : responsesql.ID_ERR;
                response.Message = responsesql.DESCR_ERR;
                response.Data = responsesql.IDDATO;
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

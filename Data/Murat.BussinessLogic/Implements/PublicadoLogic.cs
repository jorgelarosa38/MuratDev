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
    public class PublicadoLogic : IPublicadoLogic
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public PublicadoLogic(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<object> GetPublicado(int cod_Operacion)
        {
            Response response = new Response();
            try
            {
                List<ExtPublicado> list = await _unitOfWork.Publicado.GetPublicado(cod_Operacion);

                if (list.Count > 0)
                {
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

        public async Task<object> GetPublicadoID(int cod_Operacion, int idDato)
        {
            Response response = new Response();
            try
            {
                object list = await _unitOfWork.Publicado.GetPublicadoID(cod_Operacion, idDato);

                if (list != null)
                {
                    string directory = _config.GetSection("AppSettings").GetSection("url_imagenes").Value;
                    if (cod_Operacion == 1396)
                    {
                        List<PublicadoProducto> lstProd = (List<PublicadoProducto>)list;
                        foreach (var Producto in lstProd)
                        {
                            if (Producto.SArchivo_Producto != "")
                            {
                                Producto.Url_Producto = AuxiliarMethods.GenerarURL(directory, "Producto", Producto.SArchivo_Producto);
                            }
                            if (Producto.Archivo_Marca != "")
                            {
                                Producto.Url_Marca = AuxiliarMethods.GenerarURL(directory, "Marca", Producto.Archivo_Marca);
                            }
                            if (Producto.SArchivo_Talla != "")
                            {
                                Producto.Url_Talla = AuxiliarMethods.GenerarURL(directory, "Producto", Producto.SArchivo_Talla);
                            }
                            foreach (var Color in Producto.Color)
                            {
                                if (Color.SArchivo_Imagen != "")
                                {
                                    Color.Url_Imagen = AuxiliarMethods.GenerarURL(directory, "Producto", Color.SArchivo_Imagen);
                                }
                            }
                            foreach (var Imagen in Producto.Imagen)
                            {
                                if (Imagen.SArchivo_Imagen_0 != "")
                                {
                                    Imagen.Url_Imagen_0 = AuxiliarMethods.GenerarURL(directory, "Producto", Imagen.SArchivo_Imagen_0);
                                }
                            }
                            foreach (var ProdImagen in Producto.Producto_Imagen)
                            {
                                if (ProdImagen.SArchivo_Imagen_0 != "")
                                {
                                    ProdImagen.Url_Imagen_0 = AuxiliarMethods.GenerarURL(directory, "Producto", ProdImagen.SArchivo_Imagen_0);
                                }
                            }
                        }
                        response.Data = lstProd;
                    }
                    else if (cod_Operacion == 1397)
                    {
                        List<PublicadoMarca> lstMarca = (List<PublicadoMarca>)list;
                        foreach (var Marca in lstMarca)
                        {
                            if (Marca.SArchivo != "")
                            {
                                Marca.Url_Marca = AuxiliarMethods.GenerarURL(directory, "Marca", Marca.SArchivo);
                            }
                        }
                        response.Data = lstMarca;
                    }
                    else if (cod_Operacion == 1398)
                    {
                        List<PublicadoSlider> lstSlider = (List<PublicadoSlider>)list;
                        foreach (var Slider in lstSlider)
                        {
                            if (Slider.SArchivo != "")
                            {
                                Slider.Url_Slider = AuxiliarMethods.GenerarURL(directory, "Slider", Slider.SArchivo);
                            }
                        }
                        response.Data = lstSlider;
                    }
                    else
                    {
                        response.Data = list;
                    }
                    response.Status = Constant.Status;
                    response.Message = Constant.Ok;
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

        public async Task<object> UpdPublicado(Publicado publicado)
        {
            Response response = new Response();
            ResponseSql responsesql = new ResponseSql();

            try
            {
                responsesql = await _unitOfWork.Publicado.UpdPublicado(publicado);
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

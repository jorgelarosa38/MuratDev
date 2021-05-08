using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Murat.BusinessLogic.Interfaces;
using Murat.BusinessLogic.Utilities;
using Murat.Models;
using Murat.UnitOfWork;

namespace Murat.BusinessLogic.Implementations
{
    public class MuratServicesLogic : IMuratServicesLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        public MuratServicesLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<object> GetMenuBar(int Tipo, int Id1, int Id2)
        {
            Response response = new Response();
            try
            {
                List<MenuBar> list = await _unitOfWork.Murat.GetMenuBar(Tipo, Id1, Id2);

                if (list.Count > 0)
                {
                    List<MenuPadre> listP = new List<MenuPadre>();
                    var IdP = 0;

                    foreach (var item in list)
                    {
                        MenuPadre Padre = new MenuPadre();
                        Padre.SectionId = item.IdPadre;

                        if (!Padre.SectionId.Equals(IdP))
                        {
                            List<MenuDetalle> listD = new List<MenuDetalle>();
                            Padre.SectionName = item.SPadre;

                            foreach (var item2 in list)
                            {
                                if (Padre.SectionId == item2.IdPadre)
                                {
                                    MenuDetalle Detalle = new MenuDetalle();
                                    Detalle.DetailId = item2.IdDetalle;
                                    Detalle.DetailName = item2.SDetalle;
                                    listD.Add(Detalle);
                                }
                            }
                            Padre.DetailMenu = listD;
                            listP.Add(Padre);

                        }
                        IdP = Padre.SectionId;
                    }

                    response.Status = Constant.Status;
                    response.Message = Constant.Ok;
                    response.Data = listP;
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

        public async Task<object> ListFiltros(int Tipo)
        {
            Response response = new Response();
            try
            {
                object list = await _unitOfWork.Murat.ListFiltros(Tipo);

                if (list != null)
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

        public async Task<object> ListPublProducto(FiltroProducto filtroProducto)
        {
            Response response = new Response();
            try
            {
                var validado = AuxiliarMethods.ValidarFiltros(filtroProducto);

                List<PublicadoProductoServ> list = await _unitOfWork.Murat.ListPublProducto(validado);

                if (list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        if (item.SArchivo_Producto != "")
                        {
                            item.Url_Producto = AuxiliarMethods.GenerarURL("Producto", item.SArchivo_Producto);
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

        public async Task<object> ListPublProductoID(int idProducto)
        {
            Response response = new Response();
            try
            {
                List<ProductoIDServ> list = await _unitOfWork.Murat.ListPublProductoID(idProducto);

                if (list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        if (item.Archivo_Marca != "")
                        {
                            item.Url_Marca = AuxiliarMethods.GenerarURL("Marca", item.Archivo_Marca);
                        }
                        if (item.Archivo_Producto != "")
                        {
                            item.Url_Producto = AuxiliarMethods.GenerarURL("Producto", item.Archivo_Producto);
                        }
                        if (item.Archivo_Talla != "")
                        {
                            item.Url_Talla = AuxiliarMethods.GenerarURL("Producto", item.Archivo_Talla);
                        }
                        foreach (var imagen in item.Imagen)
                        {
                            if (imagen.Archivo_Produccto_Color != "")
                            {
                                imagen.Url_Color = AuxiliarMethods.GenerarURL("Producto", imagen.Archivo_Produccto_Color);
                            }
                        }
                        foreach (var color in item.Color)
                        {
                            if (color.SArchivo_Imagen_0 != "")
                            {
                                color.Url_Imagen_0 = AuxiliarMethods.GenerarURL("Producto", color.SArchivo_Imagen_0);
                            }
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

        public async Task<object> ListSlider(int Tipo, int Id1, int Id2)
        {
            Response response = new Response();
            try
            {
                List<Main> list = await _unitOfWork.Murat.ListSlider(Tipo, Id1, Id2);

                if (list.Count > 0)
                {
                    foreach (var main in list)
                    {
                        foreach (var slider in main.Sliders)
                        {
                            if (slider.SArchivo != "")
                            {
                                slider.UrlImagen = AuxiliarMethods.GenerarURL("Slider", slider.SArchivo);
                            }
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

        public async Task<object> UpdClientes(MuratClientes clientes)
        {
            Response response = new Response();
            ResponseSql responsesql = new ResponseSql();

            try
            {

                responsesql = await _unitOfWork.Murat.UpdClientes(clientes);
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

        public async Task<object> UpdPedido(MuratPedidos pedidos)
        {
            Response response = new Response();
            ResponseSql responsesql = new ResponseSql();

            try
            {
                var xml = AuxiliarMethods.ObjectToXML(pedidos);
                responsesql = await _unitOfWork.Murat.UpdPedido(xml);
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

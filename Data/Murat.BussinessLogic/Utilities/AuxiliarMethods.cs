using Murat.Models;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Murat.BusinessLogic.Utilities
{
    public class AuxiliarMethods
    {
        public static string ImageToBase64(string ImagenPath)
        {
            string path = "../Imagenes/Slider/Producto/" + ImagenPath;
            byte[] imageArray = File.ReadAllBytes(path);
            return Convert.ToBase64String(imageArray);
        }

        public static void Base64ToImage(string binary, string ImagePath, string Category)
        {
            var img = Image.FromStream(new MemoryStream(Convert.FromBase64String(binary)));
            var path = String.Concat("D:/WEB/Murat/Slider/", Category,"/", ImagePath);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            img.Save(path);
        }

        public static string GenerarURL(string categoria, string ImageName)
        {
            string url = Path.Combine(Constant.url_imagenes, categoria, ImageName);
            return url;
        }

        internal static FiltroProducto ValidarFiltros(FiltroProducto filtroProducto)
        {
            filtroProducto.STag = filtroProducto.STag.Trim().ToUpper();
            if (!((filtroProducto.IdCategoria.Trim()).Length > 0))
                filtroProducto.IdCategoria = "000";

            if (!((filtroProducto.IdMarca.Trim()).Length > 0))
                filtroProducto.IdMarca = "000";

            if (filtroProducto.Precio_Fin == 0)
                filtroProducto.Precio_Fin = 9999;

            return filtroProducto;
        }

        public static object ValidateParameters(object obj, Type model)
        {
            PropertyInfo[] properties = (model).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var Valor = property.GetValue(obj);
                if (Valor == null)
                {

                    var type = (property.Name).GetType().ToString();
                    switch (type)
                    {
                        case "System.int":

                            property.SetValue(obj, 0);
                            break;
                        case "System.String":
                            property.SetValue(obj, "");
                            break;
                    }
                }
            }
            return obj;
        }

        public static string ObjectToXML(MuratPedidos obj)
        {
            string cabecera1 = "<PAXLST_Message ><Cabecera><IDCLIENTE>" + obj.IdCliente + "</IDCLIENTE><SCORREO>" + obj.SCorreo + "</SCORREO><SNRO_CELULAR>" + obj.SNro_Celular + "</SNRO_CELULAR><SNOMBRE>" + obj.SNro_Celular + "</SNOMBRE>";
            string cabecera2 = "<SAPELLIDO>" + obj.SApellido + "</SAPELLIDO><IDPAIS>" + obj.IdPais + "</IDPAIS><UBIGEO>" + obj.Ubigeo + "</UBIGEO><SDIRECCION>" + obj.SDireccion + "</SDIRECCION><SREFERENCIA>" + obj.SReferencia + "</SREFERENCIA>";
            string cabecera3 = "<COD_DOCTO_VENTA>" + obj.Cod_Docto_Venta + "</COD_DOCTO_VENTA><COD_DOCTO_IDENTIDAD>" + obj.Cod_Docto_Identidad + "</COD_DOCTO_IDENTIDAD><SNRO_DOCTO>" + obj.SNro_Docto + "</SNRO_DOCTO>";
            string cabecera4 = "<NMAYOR_EDAD>" + obj.NMayor_Edad + "</NMAYOR_EDAD><DFECHA>" + obj.DFecha + "</DFECHA><COD_MONEDA>" + obj.Cod_Moneda + "</COD_MONEDA><NIMPORTE>" + obj.NImporte + "</NIMPORTE>";
            string cabecera5 = "<NIMPORTE_ENVIO>" + obj.NImporte_Envio + "</NIMPORTE_ENVIO><NTOTAL>" + obj.NTotal + "</NTOTAL><NRO_OPERACION>" + obj.Nro_Operacion + "</NRO_OPERACION></Cabecera>";

            string xml = String.Concat(cabecera1, cabecera2, cabecera3, cabecera4, cabecera5);

            foreach (var pedido in obj.Detalle)
            {
                string detalle = "<Detalle><IDPRODUCTO>" + pedido.IdProducto + "</IDPRODUCTO><COD_COLOR>" + pedido.Cod_Color + "</COD_COLOR><COD_TALLA>" + pedido.Cod_Talla + "</COD_TALLA><NCANTIDAD>" +
                    pedido.NCantidad + "</NCANTIDAD><NPRECIO>" + pedido.NPrecio + "</NPRECIO><IDPPUBLIC_PRODUCTO>" + pedido.IdPPublic_Producto + "</IDPPUBLIC_PRODUCTO></Detalle>";

                xml = String.Concat(xml, detalle);
            }
            string cierre_xml = "</PAXLST_Message >";
            xml = String.Concat(xml, cierre_xml);

            return xml;
        }
    }
}

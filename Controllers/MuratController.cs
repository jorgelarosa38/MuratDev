using Microsoft.AspNetCore.Mvc;
using static Murat.Utilities.Constant;
using Murat.Utilities;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Murat.BusinessLogic.Interfaces;
using Murat.Models;

namespace Murat.WebApi.ServicesControllers
{
    [Produces("application/json")]
    [Route("api/[controller]/Services")]
    [AllowAnonymous]
    [ApiController]
    public class MuratController : Controller
    {
        private readonly IMuratServicesLogic _muratserviceslogic;
        private readonly IComboLogic _comboLogic;
        public MuratController(IMuratServicesLogic muratserviceslogic, IComboLogic comboLogic)
        {
            _muratserviceslogic = muratserviceslogic;
            _comboLogic = comboLogic;
        }

        [HttpGet]
        [Route("GetMenuBar/{Tipo:int}/{Id1:int}/{Id2:int}")]
        public async Task<ActionResult<Response>> GetMenuBar(int Tipo, int Id1, int Id2)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _muratserviceslogic.GetMenuBar(Tipo, Id1, Id2);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpGet]
        [Route("ListSlider/{Tipo:int}/{Id1:int}/{Id2:int}")]
        public async Task<ActionResult<Response>> ListSlider(int Tipo, int Id1, int Id2)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _muratserviceslogic.ListSlider(Tipo, Id1, Id2);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpPost]
        [Route("UpdClientes")]
        public async Task<ActionResult<Response>> UpdClientes(MuratClientes clientes)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _muratserviceslogic.UpdClientes(clientes);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpPost]
        [Route("UpdPedido")]
        public async Task<ActionResult<Response>> UpdPedido(MuratPedidos pedidos)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _muratserviceslogic.UpdPedido(pedidos);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpPost]
        [Route("ListPublProducto")]
        public async Task<ActionResult<Response>> ListPublProducto(FiltroProducto filtroProducto)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                filtroProducto = (FiltroProducto)BusinessLogic.Utilities.AuxiliarMethods.ValidateParameters(filtroProducto, filtroProducto.GetType());
                rpta = await _muratserviceslogic.ListPublProducto(filtroProducto);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpGet]
        [Route("ListPublProductoID/{IdProducto:int}")]
        public async Task<ActionResult<Response>> ListPublProductoID(int IdProducto)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _muratserviceslogic.ListPublProductoID(IdProducto);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpGet]
        [Route("ListFiltros/{Tipo:int}")]
        public async Task<ActionResult<Response>> ListFiltros(int Tipo)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _muratserviceslogic.ListFiltros(Tipo);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpGet]
        [Route("ListarCombo/{TIPO:int}/{PARM1:maxlength(50)}/{PARM2:maxlength(50)}/{PARM3:maxlength(50)}/{PARM4:maxlength(50)}/{VALOR:int}")]
        public async Task<ActionResult<Response>> ListarCombo(int TIPO, string PARM1, string PARM2, string PARM3, string PARM4, int VALOR)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _comboLogic.ListarCombo(TIPO, PARM1, PARM2, PARM3, PARM4, VALOR);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }
    }
}

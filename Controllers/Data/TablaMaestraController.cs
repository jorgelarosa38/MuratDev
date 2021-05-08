using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Murat.BusinessLogic.Interfaces;
using Murat.Models;
using Murat.Utilities;
using System;
using System.Threading.Tasks;

namespace Murat.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TablaMaestraController : Controller
    {
        private readonly ITablaMaestraLogic _tablamaestralogic;
        public TablaMaestraController(ITablaMaestraLogic tablaMaestraLogic)
        {
            _tablamaestralogic = tablaMaestraLogic;
        }

        [HttpGet]
        [Route("GetTablaMaestra/{IDTABLA:int}")]
        public async Task<ActionResult<Response>> GetTablaMaestra(int IDTABLA)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _tablamaestralogic.GetTablaMaestra(IDTABLA);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Constant.Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpGet]
        [Route("GetListaTabla/{IDTABLA:int}/{IDPADRE:int}/{SDETALLE:maxlength(100)}")]
        public async Task<ActionResult<Response>> GetListaTabla(int IDTABLA, int IDPADRE, int SDETALLE)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _tablamaestralogic.GetListaTabla(IDTABLA, IDPADRE, SDETALLE);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Constant.Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpGet]
        [Route("GetListaTablaId/{IDDETALLE:int}")]
        public async Task<ActionResult<Response>> GetListaTablaId(int IDDETALLE)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _tablamaestralogic.GetListaTablaId(IDDETALLE);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Constant.Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }

        [HttpPost]
        [Route("PostTabla")]
        public async Task<ActionResult<Response>> PostTabla([FromBody] TablaMaestra tablaMaestra)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _tablamaestralogic.PostTabla(tablaMaestra);

                if (rpta == null)
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                response.Status = Constant.Error500;
                response.Message = e.Message;
                return Ok(response);
            }
            return Ok(rpta);
        }
    }
}

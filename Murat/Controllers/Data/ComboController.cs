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
    public class ComboController : Controller
    {
        private readonly IComboLogic _combologic;
        public ComboController(IComboLogic combologic)
        {
            _combologic = combologic;
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

                rpta = await _combologic.ListarCombo(TIPO, PARM1, PARM2, PARM3, PARM4, VALOR);

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

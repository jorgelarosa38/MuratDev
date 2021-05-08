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
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLogic _userlogic;

        public UserController(IUserLogic userlogic)
        {
            _userlogic = userlogic;
        }

        [HttpGet]
        [Route("GetUserFilter/{page:int}/{rows:int}/{userlogin:maxlength(20)}/{name:maxlength(120)}/{estate:int}")]
        public async Task<ActionResult<Response>> GetUserFilter(int page, int rows, string userlogin, string name, string estate)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                rpta = await _userlogic.UserFilter(page, rows, userlogin, name, estate);

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
        [Route("GetUserId/{userid:int}")]
        public async Task<ActionResult<Response>> GetUserId(int userid)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _userlogic.GetUserId(userid);

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
        [Route("PostUserMnt")]
        public async Task<ActionResult<Response>> PostUserMnt([FromBody] User user)
        {
            Response response = new Response();
            object rpta = new object();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                rpta = await _userlogic.UserMnt(user);

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
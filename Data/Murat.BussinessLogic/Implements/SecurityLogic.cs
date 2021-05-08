﻿using Microsoft.Extensions.Options;
using Murat.BusinessLogic.Helpers;
using Murat.BusinessLogic.Interfaces;
using Murat.BusinessLogic.Utilities;
using Murat.UnitOfWork;
using Murat.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Murat.BusinessLogic.Implementations
{
    public class SecurityLogic : ISecurityLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;

        public SecurityLogic(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }
        public async Task<Response> ValidarAccesos(Credenciales credenciales)
        {
            Response response = new Response();
            OauthAccess oauth = new OauthAccess();
            try
            {
                List<DetalleUsuario> list = await _unitOfWork.Security.ValidarAccesos(credenciales);

                if (list != null)
                {
                    var secret = _appSettings.Secret;
                    string token = TokenGenerator.GenerateToken(list[0], secret);
                    oauth.token = token;
                    oauth.UserAccess = list;
                    response.Status = Constant.Status;
                    response.Message = Constant.Ok;
                    response.Data = oauth;
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

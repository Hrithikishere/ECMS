using BLL.Services;
using ECMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMS.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/login")]
        public HttpResponseMessage Login(LoginModel loginModel)
        {
            try
            {
                var res = AuthService.Authenticate(loginModel.Username, loginModel.Password);
                if (res != null) {

                    return Request.CreateResponse(HttpStatusCode.OK, res);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "User not found" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
        /*
        [HttpPost]
        [Route("api/logout")]
        public HttpResponseMessage Login(LoginModel loginModel)
        {
            try
            {
                var res = AuthService.Authenticate(loginModel.Username, loginModel.Password);
                if (res != null) {

                    return Request.CreateResponse(HttpStatusCode.OK, res);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "User not found" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
        */
    }
}

using BLL.Services;
using ECMS.Auth;
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

        [HttpPost]
        [Route("api/isadmin")]
        public HttpResponseMessage IsUserAdmin(string tkey)
        {
            try
            {
                var res = AuthService.IsUserAdmin(tkey);
                if (res) {

                    return Request.CreateResponse(HttpStatusCode.OK, res);

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
        
        [HttpPost]
        //[Logged]
        [Route("api/logout")]
        public HttpResponseMessage Logout(LogoutModel logoutModel)
        {
            try
            {
                var res = AuthService.Logout(logoutModel.Token);
                if (res) {

                    return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Logout Successful" });

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "Something wrong in token" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }
        
    }
}

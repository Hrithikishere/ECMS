using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMS.Controllers
{
    /// <summary>
    /// need to fix 
    /// </summary>
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("api/users")]
        public HttpResponseMessage Users()
        {
            try
            {
                var data = UserService.Read();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("api/users/{id}")]
        public HttpResponseMessage Users(int id)
        {
            try
            {
                var data = UserService.Read(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/users/create")]
        public HttpResponseMessage Users(UserDTO userDTO)
        {
            try
            {
                var data = UserService.Create(userDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/users/delete/{id}")]
        public HttpResponseMessage UsersDelete(int id)
        {
            try
            {
                var data = UserService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

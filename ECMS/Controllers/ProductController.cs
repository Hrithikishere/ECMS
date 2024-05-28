using BLL.DTOs;
using BLL.Services;
using ECMS.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ECMS.Controllers
{
    public class ProductController : ApiController
    {
        [HttpGet]
        [Route("api/products")]
        public HttpResponseMessage Products()
        {
            try
            {
                var data = ProductService.Read();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("api/products/{id}")]
        public HttpResponseMessage Products(int id)
        {
            try
            {
                var data = ProductService.Read(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        //[Logged]
        //[Admin]
        [Route("api/products/create")]
        public HttpResponseMessage Products(ProductDTO productDTO)
        {
            try
            {
                var data = ProductService.Create(productDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpPost]
        //[Logged]
        //[Admin]
        [Route("api/products/update")]
        public HttpResponseMessage ProductsUpdate(ProductDTO productDTO)
        {
            try
            {
                var data = ProductService.Update(productDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        //[Logged]
        //[Admin]
        [Route("api/products/delete/{id}")]
        public HttpResponseMessage ProductsDelete(int id)
        {
            try
            {
                var data = ProductService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        //[Logged]
        //[Admin]
        [Route("api/products/inventorylogs")]
        public HttpResponseMessage ProductsWithInventoryLogs()
        {
            try
            {
                var data = ProductService.ProductsWithInventoryLogs();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("api/products/inventorylogs/{id}")]
        public HttpResponseMessage ProductsWithInventoryLogs(int id)
        {
            try
            {
                var data = ProductService.ProductsWithInventoryLogs(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        //[Logged]
        //[Admin]
        [Route("api/products/orderitems")]
        public HttpResponseMessage ProductsWithOrderItems()
        {
            try
            {
                var data = ProductService.ProductsWithOrderItems();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        //[Logged]
        //[Admin]
        [Route("api/products/orderitems/{id}")]
        public HttpResponseMessage ProductsWithOrderItems(int id)
        {
            try
            {
                var data = ProductService.ProductsWithOrderItems(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

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
    public class OrderController : ApiController
    {
        [HttpGet]
        [Route("api/order")]
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

        [HttpPost]
        [Route("api/order/create")]
        public HttpResponseMessage Products(OrderOrderItemsDTO orderOrderItemsDTO)
        {
            try
            {
                var data = OrderService.PlaceOrder(orderOrderItemsDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}

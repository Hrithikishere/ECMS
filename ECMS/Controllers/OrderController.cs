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
        [Route("api/orders")]
        public HttpResponseMessage Orders()
        {
            try
            {
                var data = OrderService.Read();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpGet]
        [Route("api/orders/{id}")]
        public HttpResponseMessage Orders(int id)
        {
            try
            {
                var data = OrderService.Read(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/orders/create")]
        public HttpResponseMessage Orders(OrderOrderItemsDTO orderOrderItemsDTO)
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

        [HttpPost]
        [Route("api/orders/update")]
        public HttpResponseMessage OrderUpdate(OrderOrderItemsDTO orderOrderItemsDTO)
        {
            try
            {
                var data = OrderService.UpdateOrder(orderOrderItemsDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

        [HttpGet]
        [Route("api/orders/orderitems")]
        public HttpResponseMessage OrdersWithOrderItems()
        {
            try
            {
                var data = OrderService.OrderWithOrderItems();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }
        
        [HttpGet]
        [Route("api/orders/orderitems/{id}")]
        public HttpResponseMessage OrdersWithOrderItems(int id)
        {
            try
            {
                var data = OrderService.OrderWithOrderItems(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

    }
}

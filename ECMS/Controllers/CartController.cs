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
    public class CartController : ApiController
    {
        [HttpGet]
        [Route("api/carts")]
        public HttpResponseMessage Carts()
        {
            try
            {
                var data = CartService.Read();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("api/carts/{id}")]
        public HttpResponseMessage Carts(int id)
        {
            try
            {
                var data = CartService.Read(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/carts/create")]
        public HttpResponseMessage Carts(CartCartItemDTO cartCartItemDTO)
        {
            try
            {
                var data = CartService.AddToCart(cartCartItemDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpPost]
        [Route("api/carts/update")]
        public HttpResponseMessage CartUpdate(CartCartItemDTO cartCartItemDTO)
        {
            try
            {
                var data = CartService.UpdateCart(cartCartItemDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

        [HttpPost]
        [Route("api/carts/delete/{id}")]
        public HttpResponseMessage CartDelete(int id)
        {
            try
            {
                var data = CartService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

        [HttpGet]
        [Route("api/carts/cartitems")]
        public HttpResponseMessage CartsWithCartItems()
        {
            try
            {
                var data = CartService.CartWithCartItems();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

        [HttpGet]
        [Route("api/carts/cartitems/{id}")]
        public HttpResponseMessage CartsWithCartItems(int id)
        {
            try
            {
                var data = CartService.CartWithCartItems(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);

            }
        }

    }
}

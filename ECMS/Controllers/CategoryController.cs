﻿using BLL.DTOs;
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
    public class CategoryController : ApiController
    {
        [HttpGet]
        [Route("api/categories")]
        public HttpResponseMessage Categories()
        {
            try
            {
                var data = CategoryService.Read();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpGet]
        [Route("api/categories/{id}")]
        public HttpResponseMessage Categories(int id)
        {
            try
            {
                var data = CategoryService.Read(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpPost]
        //[Admin]
        //[Logged]
        [Route("api/categories/create")]
        public HttpResponseMessage Categories(CategoryDTO categoryDTO)
        {
            try
            {
                var data = CategoryService.Create(categoryDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpPost]
        //[Admin]
        //[Logged]
        [Route("api/categories/update")]
        public HttpResponseMessage CategoriesUpdate(CategoryDTO categoryDTO)
        {
            try
            {
                var data = CategoryService.Update(categoryDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpDelete]
        //[Admin]
        //[Logged]
        [Route("api/categories/delete/{id}")]
        public HttpResponseMessage CategoriesDelete(int id)
        {
            try
            {
                var data = CategoryService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpGet]
        [Route("api/categories/products")]
        public HttpResponseMessage CategoriesWithProducts()
        {
            try
            {
                var data = CategoryService.CategoryWithProducts();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new {Message = ex.Message});
            }
        }

        [HttpGet]
        [Route("api/categories/products/{id}")]
        public HttpResponseMessage CategoriesWithProducts(int Id)
        {
            try
            {
                var data = CategoryService.CategoryWithProducts(Id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new {Message = ex.Message});
            }
        }
    }
}

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
    public class InventoryLogController : ApiController
    {
        [HttpGet]
        [Route("api/inventorylogs")]
        public HttpResponseMessage InventoryLogs()
        {
            try
            {
                var data = InventoryLogService.Read();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("api/inventorylogs/{id}")]
        public HttpResponseMessage InventoryLogs(int id)
        {
            try
            {
                var data = InventoryLogService.Read(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/inventorylogs/create")]
        public HttpResponseMessage InventoryLogs(InventoryLogDTO inventoryLogDTO)
        {
            try
            {
                var data = InventoryLogService.Create(inventoryLogDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
        
        [HttpPost]
        [Route("api/inventorylogs/update")]
        public HttpResponseMessage InventoryLogsUpdate(InventoryLogDTO inventoryLogDTO)
        {
            try
            {
                var data = InventoryLogService.Update(inventoryLogDTO);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/inventorylogs/delete/{id}")]
        public HttpResponseMessage InventoryLogsDelete(int id)
        {
            try
            {
                var data = InventoryLogService.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}

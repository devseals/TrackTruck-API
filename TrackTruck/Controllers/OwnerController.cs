using AutoMapper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrackTruck.dto;
using TrackTruck.Models;

namespace TrackTruck.Controllers
{
    [Authorize]
    [Route("api/owners")]
    public class OwnerController : BaseController
    {
        [HttpGet]
        [Route("api/owners/{id}")]
        public IHttpActionResult GetId(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var owner = context.owners.FirstOrDefault(c => c.owner_id == id);
                if (owner == null)
                {
                    response.Status = Helpers.ResponseStatus.ERROR;
                    response.Message = string.Format(Helpers.ErrorMessage.NOT_FOUND, "owner", id);
                    return Content(HttpStatusCode.NotFound, response);
                }
                response.Status = Helpers.ResponseStatus.OK;
                response.Owners = Mapper.Map<owners, OwnerDTO>(owner);
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = Helpers.ResponseStatus.ERROR;
                response.Message = Helpers.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            dynamic response = new ExpandoObject();
            try
            {
                response.Status = Helpers.ResponseStatus.OK;
                var owners = context.owners.ToList();
                response.Code = HttpStatusCode.OK;
                response.Owners = owners.Select(Mapper.Map<owners, OwnerDTO>);
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = Helpers.ResponseStatus.ERROR;
                response.Message = Helpers.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}

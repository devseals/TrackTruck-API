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
    [AllowAnonymous]
    [RoutePrefix("api/register")]
    public class RegisterController : BaseController
    {
        [HttpPost]
        [Route("user")]
        public IHttpActionResult CreateUser(users user)
        {
            dynamic Response = new ExpandoObject();
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = Helpers.ResponseStatus.ERROR;
                    Response.Message = Helpers.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var userDTO = Mapper.Map<users, UserDTO>(user);

                user.password = SecurePasswordHasher.Hash(user.password);
                context.users.Add(user);
                context.SaveChanges();

                Response.Status = Helpers.ResponseStatus.OK;
                Response.Users = userDTO;

                return Ok(Response);

            }
            catch (Exception)
            {
                Response.Status = Helpers.ResponseStatus.ERROR;
                Response.Message = Helpers.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }

        [HttpPost]
        [Route("owner")]
        public IHttpActionResult CreateOwner(owners owner)
        {
            dynamic Response = new ExpandoObject();
            try
            {
                if (!ModelState.IsValid)
                {
                    Response.Status = Helpers.ResponseStatus.ERROR;
                    Response.Message = Helpers.ErrorMessage.BAD_REQUEST;
                    return Content(HttpStatusCode.BadRequest, Response);
                }

                var ownerDTO = Mapper.Map<owners, OwnerDTO>(owner);

                owner.password = SecurePasswordHasher.Hash(owner.password);
                context.owners.Add(owner);
                context.SaveChanges();

                Response.Status = Helpers.ResponseStatus.OK;
                Response.Owners = ownerDTO;

                return Ok(Response);

            }
            catch (Exception)
            {
                Response.Status = Helpers.ResponseStatus.ERROR;
                Response.Message = Helpers.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
        }
    }
}

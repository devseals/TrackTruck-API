using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    [Route("api/users")]
    public class UserController : BaseController
    {
        [HttpGet]
        [Route("api/users/{id}")]
        public IHttpActionResult GetId(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var user = context.users.FirstOrDefault(c => c.user_id == id);
                if (user == null)
                {
                    response.Status = Helpers.ResponseStatus.ERROR;
                    response.Message = string.Format(Helpers.ErrorMessage.NOT_FOUND, "user", id);
                    return Content(HttpStatusCode.NotFound, response);
                }
                response.Status = Helpers.ResponseStatus.OK;
                response.Users = Mapper.Map<users, UserDTO>(user);
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
                var users = context.users.ToList();
                response.Code = HttpStatusCode.OK;
                response.Users = users.Select(Mapper.Map<users, UserDTO>);
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = Helpers.ResponseStatus.ERROR;
                response.Message = Helpers.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, response);
            }            
        }

        [HttpPut]
        [Route("api/users/{id}")]
        public IHttpActionResult Update(int id,users user)
        {
            if (id != user.user_id)
            {
                return BadRequest();
            }
            try
            {
                user.password = SecurePasswordHasher.Hash(user.password);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return Ok(user);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }            
        }

        [HttpDelete]
        [Route("api/users/{id}")]
        public IHttpActionResult Delete(long id)
        {
            var user = context.users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            context.users.Remove(user);
            context.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

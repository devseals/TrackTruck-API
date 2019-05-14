using AutoMapper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using TrackTruck.dto;
using TrackTruck.Models;

namespace TrackTruck.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : BaseController
    {
        
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        [HttpPost]
        [Route("user")]
        public IHttpActionResult AuthenticateUser(LoginRequest login)
        {
            dynamic Response = new ExpandoObject();
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var user = context.users.Where(x=>x.username==login.Username).FirstOrDefault();
            
            if (SecurePasswordHasher.Verify(login.Password, user.password))
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                Response.Status = Helpers.ResponseStatus.OK;
                Response.User = Mapper.Map<users, UserDTO>(user);
                Response.Token = token;
                return Ok(Response);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("owner")]
        public IHttpActionResult AuthenticateOwner(LoginRequest login)
        {
            dynamic Response = new ExpandoObject();
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var owner = context.owners.Where(x => x.username == login.Username).FirstOrDefault();
            
            if (SecurePasswordHasher.Verify(login.Password, owner.password))
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                Response.Status = Helpers.ResponseStatus.OK;
                Response.Owner = Mapper.Map<owners, OwnerDTO>(owner);
                Response.Token = token;
                return Ok(Response);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("employee")]
        public IHttpActionResult AuthenticateEmployee(LoginRequest login)
        {
            dynamic Response = new ExpandoObject();
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var employee = context.employees.Where(x => x.username == login.Username).FirstOrDefault();
            
            if (SecurePasswordHasher.Verify(login.Password, employee.password))
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                Response.Status = Helpers.ResponseStatus.OK;                
                Response.Employee = Mapper.Map<employees, EmployeeDTO>(employee);
                Response.Token = token;
                return Ok(Response);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

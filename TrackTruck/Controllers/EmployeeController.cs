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
    [Route("api/employees")]
    public class EmployeeController : BaseController
    {
        [HttpGet]
        [Route("api/employees/{id}")]
        public IHttpActionResult GetId(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var employee = context.employees.FirstOrDefault(c => c.employee_id == id);
                if (employee == null)
                {
                    response.Status = Helpers.ResponseStatus.ERROR;
                    response.Message = string.Format(Helpers.ErrorMessage.NOT_FOUND, "employee", id);
                    return Content(HttpStatusCode.NotFound, response);
                }
                response.Status = Helpers.ResponseStatus.OK;
                response.Employees = Mapper.Map<employees, EmployeeDTO>(employee);
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
                var employees = context.employees.ToList();
                response.Code = HttpStatusCode.OK;
                response.Employees = employees.Select(Mapper.Map<employees, EmployeeDTO>);
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = Helpers.ResponseStatus.ERROR;
                response.Message = Helpers.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, response);
            }
        }

        [HttpPost]
        public IHttpActionResult Create(employees employee)
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

                var employeeDTO = Mapper.Map<employees, EmployeeDTO>(employee);

                employee.password = SecurePasswordHasher.Hash(employee.password);
                context.employees.Add(employee);
                context.SaveChanges();

                Response.Status = Helpers.ResponseStatus.OK;
                Response.Employees = employeeDTO;

                return Ok(Response);

            }
            catch (Exception)
            {
                Response.Status = Helpers.ResponseStatus.ERROR;
                Response.Message = Helpers.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, Response);
            }
            /*try
            {
                employee.password = SecurePasswordHasher.Hash(employee.password);
                context.employees.Add(employee);
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return Ok(employee);*/
        }

    }
}

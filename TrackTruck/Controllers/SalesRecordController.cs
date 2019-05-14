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
    //[Route("api/salesrecords")]
    [RoutePrefix("api/sales")]
    public class SalesRecordController : BaseController
    {
        [HttpGet]
        //[Route("api/salesrecords/{id}")]
        [Route("{id:int}")]
        public IHttpActionResult GetId(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var sale = context.sales_records.FirstOrDefault(c => c.sales_record_id == id);
                if (sale == null)
                {
                    response.Status = Helpers.ResponseStatus.ERROR;
                    response.Message = string.Format(Helpers.ErrorMessage.NOT_FOUND, "sales record", id);
                    return Content(HttpStatusCode.NotFound, response);
                }
                response.Status = Helpers.ResponseStatus.OK;
                response.Sales = Mapper.Map<sales_records, SalesRecordDTO>(sale);
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
        [Route("owners/{ownerId}")]
        public IHttpActionResult GetSalesrecordsByOwner(int ownerId)
        {
            dynamic response = new ExpandoObject();
            try
            {
                response.Status = Helpers.ResponseStatus.OK;
                var sales = context.sales_records
                    .Join(context.employees,x=>x.employee_id,y=>y.employee_id, (x, y) => new { Sales = x, Employee = y })
                    .Where(x=>x.Employee.owner_id==ownerId)
                    .Select(x=> new {
                        sales_record_id = x.Sales.sales_record_id,
                        date =x.Sales.date,
                        value = (float)x.Sales.value                        
                    })
                    .ToList();
                response.Code = HttpStatusCode.OK;
                response.Sales = sales;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Status = Helpers.ResponseStatus.ERROR;
                response.Message = Helpers.ErrorMessage.INTERNAL_SERVER_ERROR;
                return Content(HttpStatusCode.InternalServerError, response);
            }
        }


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            dynamic response = new ExpandoObject();
            try
            {
                response.Status = Helpers.ResponseStatus.OK;
                var sales = context.sales_records.ToList();
                response.Code = HttpStatusCode.OK;
                response.SalesRecords = sales.Select(Mapper.Map<sales_records, SalesRecordDTO>);
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
        [Route("")]
        public IHttpActionResult Create(sales_records sale)
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

                var saleDTO = Mapper.Map<sales_records, SalesRecordDTO>(sale);

                context.sales_records.Add(sale);
                context.SaveChanges();

                Response.Status = Helpers.ResponseStatus.OK;
                Response.SalesRecords = saleDTO;

                return Created(new Uri(Request.RequestUri + "/" + saleDTO.sales_record_id), Response);

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

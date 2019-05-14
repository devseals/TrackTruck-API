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
    [Route("api/foodtrucks")]
    public class FoodTruckController : BaseController
    {
        [HttpGet]
        [Route("api/foodtrucks/{id}")]
        public IHttpActionResult GetId(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var foodtruck = context.foodtrucks.FirstOrDefault(c => c.foodtruck_id == id);
                if (foodtruck == null)
                {
                    response.Status = Helpers.ResponseStatus.ERROR;
                    response.Message = string.Format(Helpers.ErrorMessage.NOT_FOUND, "foodtruck", id);
                    return Content(HttpStatusCode.NotFound, response);
                }
                response.Status = Helpers.ResponseStatus.OK;
                response.Foodtrucks = Mapper.Map<foodtrucks, FoodtruckDTO>(foodtruck);
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
                var foodtrucks = context.foodtrucks.ToList();
                response.Code = HttpStatusCode.OK;
                response.Foodtrucks = foodtrucks.Select(Mapper.Map<foodtrucks, FoodtruckDTO>);
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
        public IHttpActionResult Create(foodtrucks foodtruck)
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

                var foodtruckDTO = Mapper.Map<foodtrucks, FoodtruckDTO>(foodtruck);

                context.foodtrucks.Add(foodtruck);
                context.SaveChanges();

                Response.Status = Helpers.ResponseStatus.OK;
                Response.SalesRecords = foodtruckDTO;

                return Created(new Uri(Request.RequestUri + "/" + foodtruckDTO.foodtruck_id), Response);

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

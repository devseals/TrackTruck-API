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
    [Route("api/reviews")]
    //[RoutePrefix("api/reviews")]
    public class ReviewController : BaseController
    {
        [HttpGet]
        [Route("api/reviews/{id}")]
        public IHttpActionResult GetId(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var review = context.reviews.FirstOrDefault(c => c.review_id == id);
                if (review == null)
                {
                    response.Status = Helpers.ResponseStatus.ERROR;
                    response.Message = string.Format(Helpers.ErrorMessage.NOT_FOUND, "review", id);
                    return Content(HttpStatusCode.NotFound, response);
                }
                response.Status = Helpers.ResponseStatus.OK;
                response.Reviews = Mapper.Map<reviews, ReviewDTO>(review);
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
                var reviews = context.reviews.ToList();
                response.Code = HttpStatusCode.OK;
                response.Reviews = reviews.Select(Mapper.Map<reviews, ReviewDTO>);
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
        public IHttpActionResult Create(reviews review)
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

                

                context.reviews.Add(review);
                context.SaveChanges();
                var reviewDTO = Mapper.Map<reviews, ReviewDTO>(review);
                reviewDTO.review_id = review.review_id;
                Response.Status = Helpers.ResponseStatus.OK;
                Response.Reviews = reviewDTO;

                return Created(new Uri(Request.RequestUri + "/" + reviewDTO.review_id), Response);

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

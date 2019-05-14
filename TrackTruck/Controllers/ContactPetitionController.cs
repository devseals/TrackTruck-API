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
    //[Route("api/contactpetitions")]
    [RoutePrefix("api/contact_petitions")]
    public class ContactPetitionController : BaseController
    {
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetId(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var contact = context.contact_petitions.FirstOrDefault(c => c.contact_petition_id == id);
                if (contact == null)
                {
                    response.Status = Helpers.ResponseStatus.ERROR;
                    response.Message = string.Format(Helpers.ErrorMessage.NOT_FOUND, "contact petition", id);
                    return Content(HttpStatusCode.NotFound, response);
                }
                response.Status = Helpers.ResponseStatus.OK;
                response.ContactPetitions = Mapper.Map<contact_petitions, ContactPetitionDTO>(contact);
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
        [Route("")]
        public IHttpActionResult GetAll()
        {
            dynamic response = new ExpandoObject();
            try
            {
                response.Status = Helpers.ResponseStatus.OK;
                var contacts = context.contact_petitions.ToList();
                response.Code = HttpStatusCode.OK;
                response.ContactPetitions = contacts.Select(Mapper.Map<contact_petitions, ContactPetitionDTO>);
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
        public IHttpActionResult Create(contact_petitions contact)
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

                var contactDTO = Mapper.Map<contact_petitions, ContactPetitionDTO>(contact);

                context.contact_petitions.Add(contact);
                context.SaveChanges();

                Response.Status = Helpers.ResponseStatus.OK;
                Response.ContactPetitions = contactDTO;

                return Created(new Uri(Request.RequestUri + "/" + contactDTO.contact_petition_id), Response);

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

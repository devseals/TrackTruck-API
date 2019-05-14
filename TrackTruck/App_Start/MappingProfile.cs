using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrackTruck.Models;

namespace TrackTruck.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<contact_petitions, dto.ContactPetitionDTO>();
            CreateMap<dto.ContactPetitionDTO, contact_petitions>();

            CreateMap<employees, dto.EmployeeDTO>();
            CreateMap<dto.EmployeeDTO, employees>();

            CreateMap<foodtrucks, dto.FoodtruckDTO>();
            CreateMap<dto.FoodtruckDTO, foodtrucks>();

            CreateMap<owners, dto.OwnerDTO>();
            CreateMap<dto.OwnerDTO, owners>();

            CreateMap<reviews, dto.ReviewDTO>();
            CreateMap<dto.ReviewDTO, reviews>();

            CreateMap<sales_records, dto.SalesRecordDTO>();
            CreateMap<dto.SalesRecordDTO, sales_records>();

            CreateMap<users, dto.UserDTO>();
            CreateMap<dto.UserDTO, users>();
        }
    }
}
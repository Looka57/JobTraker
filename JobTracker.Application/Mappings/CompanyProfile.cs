using AutoMapper;
using JobTracker.Application.DTOs;
using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyReadDto>();
            CreateMap<CompanyCreateDto, Company>();
            CreateMap<Company, CompanyUpdateDto>();
        }
    }
}

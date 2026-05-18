using AutoMapper;
using JobTracker.Application.DTOs;
using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobTracker.Application.Mappings
{

    public class CandidatureProfile : Profile
    {
        public CandidatureProfile()
        {
            CreateMap<Candidature, CandidatureReadDto>()
                        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Company.Name));
            CreateMap<CandidatureCreateDto, Candidature>();
            CreateMap<CandidatureUpdateDto, Candidature>();

            CreateMap<CandidatureCreateDto, Candidature>()
                        .ForMember(dest => dest.CompanyId, opt => opt.Ignore());
        }
    }

}
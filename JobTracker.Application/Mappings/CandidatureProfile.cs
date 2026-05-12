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
            CreateMap<Candidature, CandidatureDto>()
                .ForMember(dest => dest.name,
                    opt => opt.MapFrom(src => src.Company!.Name));

            CreateMap<CreateCandidatureDto, Candidature>();

            CreateMap<UpdateCandidatureDto, Candidature>();
        }
    }

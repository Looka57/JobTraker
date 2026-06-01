using AutoMapper;
using JobTracker.Application.DTOs;
using JobTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobTracker.Application.Mappings
{
    public class InteractionProfile : Profile
    {

        public InteractionProfile()
        {
            CreateMap<Interaction, InteractionReadDto>()
                // On lie la propriété CompanyName du DTO au Name de la Company
                .ForMember(dest => dest.CompanyName,
                           opt => opt.MapFrom(src => src.Candidature!.Company!.Name));
        //Note: Le point d'exclamation ! (null-forgiving operator) indique au compilateur :
        //"Ne t'inquiète pas, j'ai fait un .Include() dans mon Repository, je sais que ces données seront chargées !".

            CreateMap<InteractionCreateDto, Interaction>();
            CreateMap<InteractionUpdateDto, Interaction>();
        }

    }
}

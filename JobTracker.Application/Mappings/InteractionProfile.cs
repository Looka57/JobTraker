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
            CreateMap<Interaction, InteractionReadDto>();
            CreateMap<InteractionCreateDto, Interaction>();
            CreateMap<InteractionUpdateDto, Interaction>();
        }

    }
}

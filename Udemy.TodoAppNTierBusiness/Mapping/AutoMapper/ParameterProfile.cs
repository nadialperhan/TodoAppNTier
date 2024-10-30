using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.ParameterDto;
using Udemy.TodoAppNTier.Entities.Domains;

namespace Udemy.TodoAppNTierBusiness.Mapping.AutoMapper
{
    public class ParameterProfile:Profile
    {
        public ParameterProfile()
        {
            CreateMap<Parameter, ParameterDropdownDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

        }

    }
}

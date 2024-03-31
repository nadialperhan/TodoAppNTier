using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using Udemy.TodoAppNTier.Entities.Domains;

namespace Udemy.TodoAppNTierBusiness.Mapping.AutoMapper
{
    public class WorkProfile:Profile
    {
        public WorkProfile()
        {
            CreateMap<Work, WorkUpdatedDto>().ReverseMap();
            CreateMap<Work, WorkListDtos>().ReverseMap();
            CreateMap<Work, WorkCreateDto>().ReverseMap();
            CreateMap<WorkListDtos, WorkUpdatedDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Common.ResponseObjects;
using Udemy.TodoAppNTier.DataAccess.UnitofWork;
using Udemy.TodoAppNTier.Dtos.Interfaces;
using Udemy.TodoAppNTier.Dtos.ParameterDto;
using Udemy.TodoAppNTier.Entities.Domains;
using Udemy.TodoAppNTierBusiness.Interfaces;

namespace Udemy.TodoAppNTierBusiness.Services
{
    public class CommonService : ICommonService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public CommonService(IUow uow,IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResponse<List<ParameterDropdownDto>>> DropDownParameter(int groupid)
        {

            var b = await _uow.GetRepository<Parameter>().GetQuery().Where(x => x.ParameterGroupId == groupid).ToListAsync();

            var data = _mapper.Map<List<ParameterDropdownDto>>(b);
            return new Response<List<ParameterDropdownDto>>(ResponseType.Success, data);
        }

       
    }
}

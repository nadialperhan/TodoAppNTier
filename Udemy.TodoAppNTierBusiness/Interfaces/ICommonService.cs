using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Common.ResponseObjects;
using Udemy.TodoAppNTier.Dtos.ParameterDto;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTierBusiness.Interfaces
{
    public interface ICommonService
    {
        Task<IResponse<List<ParameterDropdownDto>>> DropDownParameter(int id);

    }
}

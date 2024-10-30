using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Common.ResponseObjects;
using Udemy.TodoAppNTier.Dtos.FilterDto;
using Udemy.TodoAppNTier.Dtos.Interfaces;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTierBusiness.Services
{
    public interface IWorkService
    {
        Task<IResponse<List<WorkListDtos>>> GetAll();

        Task<IResponse<WorkCreateDto>> Create(WorkCreateDto createDto);

        Task<IResponse<IDto>> GetbyID<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<WorkUpdatedDto>> Update(WorkUpdatedDto dto);
        Task<IResponse<List<WorkListDto>>> GetWorkDataUsingStoredProcedure(IndexFilterDto dto);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.FilterDto;
using Udemy.TodoAppNTier.Dtos.WorkDtos;
using X.PagedList;

namespace Udemy.TodoAppNTier.UI.ViewModel
{
    public class WorkFilterModel
    {
        public IPagedList<WorkListDto> WorkListDto { get; set; }
        public IndexFilterDto IndexFilterDto { get; set; }
    }
}

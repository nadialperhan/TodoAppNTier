using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTierBusiness.ValidationRules
{
    public class WorkUpdateDtoValidator : AbstractValidator<WorkUpdatedDto>
    {
        public WorkUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

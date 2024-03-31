using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTierBusiness.ValidationRules
{
    public class WorkListDtoValidator:AbstractValidator<WorkListDtos>
    {
        public WorkListDtoValidator()
        {

        }
    }
}

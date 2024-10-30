using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.Interfaces;

namespace Udemy.TodoAppNTier.Dtos.ParameterDto
{
    public class ParameterDropdownDto:IDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}

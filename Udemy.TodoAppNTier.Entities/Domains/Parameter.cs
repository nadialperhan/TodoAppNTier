using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.Entities.Domains
{
    public class Parameter:BaseEntity
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public ParameterGroup ParameterGroup { get; set; }
        public int ParameterGroupId { get; set; }
    }
}

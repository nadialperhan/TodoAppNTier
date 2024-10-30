using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.Entities.Domains
{
    public class ParameterGroup:BaseEntity
    {
        public string ParameterGroupName { get; set; }
        public ICollection<Parameter> Parameter { get; set; }
    }
}
